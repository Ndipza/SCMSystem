using Core.ViewModels;

namespace RepositoriesTest.MockData
{
    public class PaymentMethodMockData
    {
        public static List<Data.Models.PaymentMethod> GetPaymentMethods()
        {
            return new List<Data.Models.PaymentMethod>
            {
                 new Data.Models.PaymentMethod{
                     Id = 1,
                     Description  = "Cash"
                 },
                 new Data.Models.PaymentMethod{
                     Id = 2,
                     Description = "Credit"
                 },
                 new Data.Models.PaymentMethod{
                     Id = 3,
                     Description = "Account"
                 }
            };
        }

        public static List<Data.Models.PaymentMethod> GetEmptyTodos()
        {
            return new List<Data.Models.PaymentMethod>();
        }

        public static PaymentMethodViewModel NewPaymentMethod()
        {
            return new PaymentMethodViewModel
            {
                Name = "Debit"
            };
        }

        public static CategoryViewModel NewPaymentMethodWithMoreThan50Characters()
        {
            return new CategoryViewModel
            {
                Name = "One thing you should notice here is that in \"Task_Add_InvalidData_Return_BadRequest\" Unit Test Cases, we are passing more than 50 characters for Description, which is not correct because in Post model, we have defined the size of the Description as 50 characters"
            };
        }
    }
}
