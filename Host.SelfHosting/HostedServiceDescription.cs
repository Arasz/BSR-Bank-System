using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;

namespace Host.SelfHosting
{
    public class HostedServiceDescription
    {
        private readonly ServiceHost _serviceHost;
        private StringBuilder _descriptioBuilder;

        private string _description;

        public string Description
        {
            get
            {
                if (_description == null)
                    Describe();

                return _description;
            }
        }

        public HostedServiceDescription(ServiceHost serviceHost)
        {
            _serviceHost = serviceHost;
            _descriptioBuilder = new StringBuilder();
        }

        private void Describe()
        {
            GeneralDescription();
            DescribeAuthenticationBehavior();
            DescribeAuthorizationBehavior();

            _description = _descriptioBuilder.ToString();
        }

        private void DescribeAuthenticationBehavior()
        {
            var authenticationBehavior = _serviceHost.Authentication;

            _descriptioBuilder.AppendLine($"# {nameof(ServiceAuthenticationBehavior)}:");
            _descriptioBuilder.AppendLine($"\t- {nameof(ServiceAuthenticationBehavior.AuthenticationSchemes)} : {authenticationBehavior.AuthenticationSchemes}");
            _descriptioBuilder.AppendLine($"\t- {nameof(ServiceAuthenticationBehavior.ServiceAuthenticationManager)} : {authenticationBehavior.ServiceAuthenticationManager}");

            _descriptioBuilder.AppendLine();
        }

        private void DescribeAuthorizationBehavior()
        {
            var authorizationBehavior = _serviceHost.Authorization;

            _descriptioBuilder.AppendLine($"# {nameof(ServiceAuthorizationBehavior)}:");
            _descriptioBuilder.AppendLine($"\t- {nameof(ServiceAuthorizationBehavior.PrincipalPermissionMode)} : {authorizationBehavior.PrincipalPermissionMode}");
            _descriptioBuilder.AppendLine($"\t- {nameof(ServiceAuthorizationBehavior.RoleProvider)} : {authorizationBehavior.RoleProvider}");
            _descriptioBuilder.AppendLine($"\t- {nameof(ServiceAuthorizationBehavior.ServiceAuthorizationManager)} : {authorizationBehavior.ServiceAuthorizationManager}");
            _descriptioBuilder.AppendLine($"\t- {nameof(ServiceAuthorizationBehavior.ImpersonateCallerForAllOperations)} : {authorizationBehavior.ImpersonateCallerForAllOperations}");

            _descriptioBuilder.AppendLine();
        }

        private void GeneralDescription()
        {
            var description = _serviceHost.Description;

            _descriptioBuilder.AppendLine($"# Name: {description.Name}");

            _descriptioBuilder.AppendLine($"# Configuration name: {description.ConfigurationName}");

            _descriptioBuilder.AppendLine($"# Service namespace: {description.Namespace}");

            _descriptioBuilder.AppendLine($"# Service type: {description.ServiceType.Name}");

            _descriptioBuilder.AppendLine($"# {nameof(description.Behaviors)}:");
            foreach (var behavior in description.Behaviors)
            {
                _descriptioBuilder.AppendLine($"\t- {behavior}");
            }

            _descriptioBuilder.AppendLine();

            _descriptioBuilder.AppendLine($"# {nameof(description.Endpoints)}:");
            foreach (var endpoint in description.Endpoints)
            {
                _descriptioBuilder.AppendLine($"\t- Name: {endpoint.Name},\n" +
                                              $"\t- Address: {endpoint.Address},\n" +
                                              $"\t- Binding: {endpoint.Binding.Name},\n" +
                                              $"\t- Contract: {endpoint.Contract.Name}");
            }

            _descriptioBuilder.AppendLine();
        }
    }
}