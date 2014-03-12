using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace Zabronim.Net.Models.Wcf.Contracts {
    public enum ServiceSecurity {
        None,
        Anonymous,
        BusinessToBusiness,
        Internet,
        Intranet
    }

    [AttributeUsage(AttributeTargets.Class)]
    internal class SecurityBehaviorAttribute : Attribute, IServiceBehavior {
        private SecurityBehavior m_SecurityBehavior;
        private string m_ApplicationName = String.Empty;
        private bool m_UseAspNetProviders;
        private bool m_ImpersonateAll;

        public SecurityBehaviorAttribute(ServiceSecurity mode) {
            m_SecurityBehavior = new SecurityBehavior(mode, "");
        }

        public SecurityBehaviorAttribute(ServiceSecurity mode,
                                         string serviceCertificateName) {
            m_SecurityBehavior =
                new SecurityBehavior(mode, serviceCertificateName);
        }

        public bool ImpersonateAll { get; set; } // m_ImpersonateAll

        public string ApplicationName { get; set; } // m_ApplicationName

        public bool UseAspNetProviders { get; set; } // m_UseAspNetProviders

        void IServiceBehavior.AddBindingParameters(
            ServiceDescription description,
            ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection parameters) {
            m_SecurityBehavior.AddBindingParameters(description, serviceHostBase, endpoints, parameters);
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) {
            throw new NotImplementedException();
        }

        void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHostBase) {
            m_SecurityBehavior.UseAspNetProviders = UseAspNetProviders;
            m_SecurityBehavior.ApplicationName = ApplicationName;
            m_SecurityBehavior.ImpersonateAll = ImpersonateAll;
            m_SecurityBehavior.Validate(description, serviceHostBase);
        }
    }

    class SecurityBehavior : IServiceBehavior {
        ServiceSecurity m_Mode;
        StoreLocation m_StoreLocation;
        StoreName m_StoreName;
        X509FindType m_FindType;
        string m_SubjectName;
        bool m_UseAspNetProviders;
        string m_ApplicationName = String.Empty;
        bool m_ImpersonateAll;

        public SecurityBehavior(ServiceSecurity mode, string serviceCertificateName) :
            this(mode, StoreLocation.LocalMachine, StoreName.My,
              X509FindType.FindBySubjectName, null) { }

        public SecurityBehavior(ServiceSecurity mode,
           StoreLocation storeLocation, StoreName storeName,
           X509FindType findType, string subjectName) { } //Sets the corresponding members

        public bool ImpersonateAll { get; set; } // m_ImpersonateAll

        public bool UseAspNetProviders { get; set; } //m_UseAspNetProviders

        public string ApplicationName { get; set; } // m_ApplicationName

        public void Validate(ServiceDescription description,
          ServiceHostBase serviceHostBase) {
            if (m_SubjectName != null) {
                switch (m_Mode) {
                    case ServiceSecurity.Anonymous:
                    case ServiceSecurity.BusinessToBusiness:
                    case ServiceSecurity.Internet:
                        string subjectName = m_SubjectName != String.Empty ?
                           m_SubjectName :
                           description.Endpoints[0].Address.Uri.Host;
                        serviceHostBase.Credentials.ServiceCertificate.
                           SetCertificate(m_StoreLocation, m_StoreName,
                             m_FindType, subjectName);
                        break;
                }
            }

        }

        public void AddBindingParameters(ServiceDescription description,
           ServiceHostBase serviceHostBase,
           Collection<ServiceEndpoint> endpoints,
           BindingParameterCollection parameters) {

            switch (m_Mode) {
                case ServiceSecurity.Intranet:
                    ConfigureIntranet(endpoints);
                    break;

                case ServiceSecurity.Internet:
                    ConfigureInternet(endpoints, UseAspNetProviders);
                    break;

            }
        }

        private void ConfigureIntranet(Collection<ServiceEndpoint> endpoints) {
            throw new NotImplementedException();
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) {
            throw new NotImplementedException();
        }

        internal static void ConfigureInternet(Collection<ServiceEndpoint> endpoints, bool useAspNetProviders) {
            foreach (ServiceEndpoint endpoint in endpoints) {
                Binding binding = endpoint.Binding;
                if (binding is WSHttpBinding) {
                    WSHttpBinding wsBinding = (WSHttpBinding)binding;
                    wsBinding.Security.Mode = SecurityMode.Message;
                    wsBinding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
                    continue;
                }

                throw new InvalidOperationException(binding.GetType() + "is unsupprted with ServiceSecurity.Internet");
            }
        }
    }
}