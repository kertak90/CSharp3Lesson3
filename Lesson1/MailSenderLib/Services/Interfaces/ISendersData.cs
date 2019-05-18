using MailSenderLib.Linq2SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderLib.Services.Interfaces
{
    public interface IrecepientsData
    {
        IEnumerable<Recepients> GetAll();
        void Write(Recepients recipient);
        void SaveChanges();
        int Create(Recepients recepients);
    }
}
