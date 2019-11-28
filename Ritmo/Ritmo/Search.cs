using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    class Search
    {
        string[] testTiteles = new string[] { "TestTitel1", "Test titel2", "Test_Titel3", "Test Titel4!", "Lorem Ipsum", "sed Dolor" };
        public List<string> searchResults( string searchRequest )
        {
            List<string> result = new List<string>();
            var geselecteerdeResults = from title in testTiteles where SqlMethods.Like(title, "%" + searchRequest + "%") select title;
            foreach(string title in geselecteerdeResults)
            {
                result.Add(title);
            }
            return result;
        }


    }
}
