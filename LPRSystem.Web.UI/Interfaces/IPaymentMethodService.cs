using LPRSystem.Web.UI.Models;

namespace LPRSystem.Web.UI.Interfaces
{
    public interface IPaymentMethodService
    {
        Task<List<PaymentMethod>> GetPaymentMethodsAsync();
        Task<PaymentMethod> GetPaymentMethodByIdAsync(long paymentmethodid);

        Task<PaymentMethod> InsertOrUpdatePaymentMethodAsync(PaymentMethod paymentMethod);

        Task<PaymentMethod> UpdatePaymentMethodAsync(PaymentMethod paymentMethod);
        Task<bool> DeletePaymentmethodAsync(long id);
    }
}
