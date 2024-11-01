using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural
{
    public class Facade: IDesignPattern
    {
        public void ExecuteDesignPattern()
        {
            Customer customer = new Customer()
            {
                Name = "Pranaya",
                Email = "info@gmail.net",
                MobileNumber = "1234567890",
                Address = "BBSR, Odisha, India"
            };

            //Using Facade Class
            CustomerRegistrationFacade customerRegistration = new CustomerRegistrationFacade();
            customerRegistration.RegisterCustomer(customer);
        }
    }

    public class CustomerRegistrationFacade
    {
        public bool RegisterCustomer(Customer customer)
        {
            //Step1: Validate the Customer
            Validator validator = new Validator();
            bool IsValid = validator.ValidateCustomer(customer);

            //Step1: Save the Customer Object into the database
            CustomerDataAccessLayer customerDataAccessLayer = new CustomerDataAccessLayer();
            bool IsSaved = customerDataAccessLayer.SaveCustomer(customer);

            //Step3: Send the Registration Email to the Customer
            Email email = new Email();
            email.SendRegistrationEmail(customer);

            return true;
        }
    }
    public class Validator
    {
        public bool ValidateCustomer(Customer customer)
        {
            return true;
        }
    }
    public class CustomerDataAccessLayer
    {
        public bool SaveCustomer(Customer customer)
        {
            return true;
        }
    }
    public class Email
    {
        public bool SendRegistrationEmail(Customer customer)
        {
            return true;
        }
    }
    public class Customer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
    }
}
