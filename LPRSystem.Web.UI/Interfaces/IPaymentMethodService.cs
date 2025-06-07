using LPRSystem.Web.UI.Models;

namespace LPRSystem.Web.UI.Interfaces
{
    public interface IPaymentMethodService
    {
        Task<List<PaymentMethod>> GetPaymentMethodsAsync();

        Task<PaymentMethod> InsertOrUpdatePaymentMethodAsync(PaymentMethod paymentMethod);

        Task<bool> DeletePaymentmethodAsync(long id);
    }
}
