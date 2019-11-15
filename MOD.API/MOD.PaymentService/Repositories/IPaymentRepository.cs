using MOD.PaymentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOD.PaymentService.Repositories
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAllPayments();
        Payment GetPaymentById(int id);
        IEnumerable<Payment> GetPaymentsForTraining(int trainingId);
        IEnumerable<Payment> GetPaymentsForMentor(int mentorId);
        int Save(Payment payment);
        int Update(Payment payment);
        bool Delete(int id);
    }
}
