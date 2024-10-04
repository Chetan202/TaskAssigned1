using Microsoft.Xrm.Sdk;
using System;

namespace TaskAssigned1
{
    public class EmailUpdateTracker : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            // Obtain the execution context from the service provider.
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            // Ensure the context contains the target entity (Contact).
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity targetEntity && targetEntity.LogicalName == "contact")
            {
                // Retrieve Pre-Image (Old Email)
                if (context.PreEntityImages.Contains("PreImage") && context.PreEntityImages["PreImage"] is Entity preImage)
                {
                    string oldEmail = preImage.Contains("emailaddress1") ? preImage["emailaddress1"].ToString() : string.Empty;

                    // Retrieve Post-Image (New Email)
                    if (targetEntity.Contains("emailaddress1"))
                    {
                        string newEmail = targetEntity["emailaddress1"].ToString();

                        // Create a new entity to update the Contact record
                        Entity updateEntity = new Entity("contact", targetEntity.Id);
                        updateEntity["contoso_oldemail"] = oldEmail;
                        updateEntity["contoso_newemail"] = newEmail;

                        // Update the Contact record with the new values
                        service.Update(updateEntity);
                    }
                }
            }
        }
    }
}
