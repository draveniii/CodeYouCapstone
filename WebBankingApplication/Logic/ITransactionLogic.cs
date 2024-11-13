using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBankingApplication.Logic
{
    public interface ITransactionLogic
    {
        Boolean WithdrawFunds(Int32 AmountToWithdraw);
        Boolean DepositFunds(Int32 AmountToDeposit);
        Boolean InternalTransfer(Int32 TransferAmount, String AccountToTransferFrom, String AccountToTransferTo);


    }
}
