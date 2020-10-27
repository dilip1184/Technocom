using System;
using System.Transactions;

namespace TechnocomShared.Helpers
{
    public class TransactionHelper : IDisposable
    {
        private TransactionScope _transactionScope;

        public TransactionHelper()
        {
            _transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew,
                                                     new TransactionOptions
                                                         {IsolationLevel = IsolationLevel.ReadCommitted});
        }

        public void Dispose()
        {
            _transactionScope.Dispose();
            _transactionScope = null;
        }

        

        public void Commit()
        {
            _transactionScope.Complete();
        }
    }
}