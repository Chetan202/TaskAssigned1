Create a plugin to display the old email address (Pre-Image) and the new email address (post-Image) in two new custom fields (oldemail and newemail) on the Contact entity when the email address is updated.
 ![image](https://github.com/user-attachments/assets/1a6ae3c3-edbd-43f3-8a27-4af38dedf845)

CODE
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

 
 ![image](https://github.com/user-attachments/assets/9f955bc6-7504-4ae2-9215-9b876b5127b7)

 

![image](https://github.com/user-attachments/assets/f2cd43ce-46a7-4900-917a-fbe87f7b3a29)

![image](https://github.com/user-attachments/assets/6d172058-3b5d-4c17-bc33-9b6110a29c4e)
