using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAssigned1
{
    public class Class1:IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            // Extract the tracing service for logging (if needed).
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // Obtain the execution context from the service provider.
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            // Ensure that the plugin is triggered on Create of Account.
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity account)
            {
                // Check if the entity is an Account
                if (account.LogicalName != "account")
                {
                    return; // Not an account entity, exit.
                }

                // Check if the Account Name field (schema name "name") is populated.
                if (!account.Attributes.Contains("name") || string.IsNullOrWhiteSpace(account["name"].ToString()))
                {
                    throw new InvalidPluginExecutionException("The Account Name field cannot be empty.");
                }

                // Check if the Telephone1 field (schema name "telephone1") is populated.
                if (!account.Attributes.Contains("telephone1") || string.IsNullOrWhiteSpace(account["telephone1"].ToString()))
                {
                    throw new InvalidPluginExecutionException("The Telephone field cannot be empty.");
                }
            }
        }
    }
}