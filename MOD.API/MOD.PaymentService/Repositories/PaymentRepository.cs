using MOD.PaymentService.Contexts;
using MOD.PaymentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOD.PaymentService.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentContext context;

        public PaymentRepository(PaymentContext context)
        {
            this.context = context;
        }

        public bool Delete(int id)
        {
            try
            {
                var paymentItem = context.Payments.Find(id);

                if (paymentItem != null)
                {
                    context.Payments.Remove(paymentItem);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Payment> GetAllPayments()
        {
            try
            {
                return context.Payments.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Payment GetPaymentById(int id)
        {
            try
            {
                return context.Payments.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Payment> GetPaymentsForMentor(int mentorId)
        {
            try
            {
                return context.Payments.Where(x => x.MentorId == mentorId).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Payment> GetPaymentsForTraining(int trainingId)
        {
            try
            {
                return
                    context.Payments.Where(
                        x => x.TrainingId == trainingId).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Save(Payment payment)
        {
            try
            {
                context.Payments.Add(payment);
                return context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Update(Payment payment)
        {
            try
            {
                var paymentStore = context.Payments.Attach(payment);
                paymentStore.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
