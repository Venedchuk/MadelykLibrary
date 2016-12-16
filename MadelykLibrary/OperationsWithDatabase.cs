using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadelykLibrary
{
    public class OperationsWithDatabase
    {

        public void AddAuthor(Author a)
        {
            using (var db = new LibraryContext())
            {
                a.Id = Guid.NewGuid();
                db.Authors.Add(a);
                db.SaveChanges();
            }
        }

    }
}
