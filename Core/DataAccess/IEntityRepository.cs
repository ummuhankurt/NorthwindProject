using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    // T 'yi sınırlandırmak istiyorum. Herkes istediği T'yi gönderemesin. Bunun adı generic constraint.
    // class : referans tip olabilir demek
    // Bu sefer buraya herhangi bir classta gelebilir. Ben sadece IEntity'den implemente olmuş classların gelmesini istiyorum çünkü öyle olması gerek.
    // interface gönderilmesin diye, new() zorunluluğu getirdik. Çünkü interfaceler newlenemez.
    public interface IEntityRepository<T> where T : class, IEntity , new()
    {
        // filter = null demek, filtre vermeyedebilirsin demek. Eğer filtre vermemişse tüm datayı istiyor demektir.
        List<T> GetAll(Expression<Func<T,bool>> filter = null); 
        void Add(T entity);
        void Update(T entity);  
        void Delete(T entity);
        T Get(Expression<Func<T, bool>> filter);
        /*List<T> GetAllByCategory(int categoryId); Normalde şu satırı silecektim ama neden Expression kullandığımı unutmamak için silmiyorum
         * Expressionun amacı bu tarz filtreleme operasyonlarını tek tek yazmak yerine tek bir operasyonda bu tarz işlemlerin hepsini
         * yapabilmektir.    
         */
    }
}
