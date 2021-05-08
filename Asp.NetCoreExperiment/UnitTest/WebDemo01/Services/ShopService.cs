using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo01.Services
{
    public interface IShopService
    {
        void FF();
    }
    public class ShopService : IShopService
    {
        private readonly IWriteDapper _writeDapper;
        private readonly IEnumerable<IReadDapper> _readDappers;
        public ShopService(IWriteDapper writeDapper, IEnumerable<IReadDapper> readDappers)
        {
            _writeDapper = writeDapper;
            _readDappers = readDappers;

        }

        public void FF()
        {
            foreach (var reader in _readDappers)
            {
                Console.WriteLine(reader.DataBaseType);
            }
        }
    }
}
