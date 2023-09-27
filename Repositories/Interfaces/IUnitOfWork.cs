using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        ICategory Category { get; }
        //IFeature Feature { get; }
        //IArticle Article { get; }
        //IHashTag HashTag { get; }
        //ILiveStream LiveStream { get; }
        //IAuthRepository Auth { get; }
        void Save();
        Task SaveAsync();
    }
}
