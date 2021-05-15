using NLayerProject.Core.NewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Core.Services
{
    public interface ISampleSqlService:IGenericService<SampleSql>//Dilersek hiç Generic Servisten kalıtım almayız ve sadece kendi methodumuzu buraya yazar öyle kullanırız
    {
    }
}
