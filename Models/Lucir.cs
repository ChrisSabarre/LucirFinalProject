using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.Data.SqlClient;

namespace LucirWeb_MVC.Models
{
    public class Lucir
    {
        //[DisplayName("item I.D. : ")]
        //public int Id { get; set; }
        //[DisplayName("Item Name : ")]
        //[StringLength(100, MinimumLength = 1,
        //    ErrorMessage = "Item Name must be 1 to 100 Characters only")]
        //public string ItemName { get; set; }

        //[DisplayName("Size : ")]
        //[StringLength(500, MinimumLength = 1,
        //    ErrorMessage = "Size must be 1 to 500 Characters only")]
        //public string Size { get; set; }

        //[DisplayName("Price Bought : ")]
        //public int PriceBought { get; set; }

        //[DisplayName("Price Sold : ")]
        //public int PriceSold { get; set; }

        [DisplayName("Username : ")]
        [StringLength(100, MinimumLength = 1,
            ErrorMessage = "Username must be 1 to 100 Characters only")]
        public string Un { get; set; }

        [DisplayName("Password : ")]
        [StringLength(100, MinimumLength = 1,
            ErrorMessage = "Pw must be 1 to 100 Characters only")]
        public string Pw { get; set; }
        public bool KLI { get; set; }
        private DAL dao = new DAL(Cs.myserver);

        public void Login()
        {
            this.V();
            dao.Open();
            dao.Set("SELECT CASE WHEN EXISTS (SELECT 1 FROM Log WHERE Username = @un AND Password = @pw) THEN 1 ELSE 0 END AS MatchFound");
            dao.Par("@un", this.Un);
            dao.Par("@pw", this.Pw);
            if(dao.Ex() != 1)
            {
                Exception ex = new Exception(
                    "Incorrect Username or Password");
                ex.Source = "Lucir Class";
                throw ex;
            }
            dao.Close();

        }

        public void V()
        {
            if(this.Un == null || this.Pw == null)
            {
                Exception ex = new Exception(
    "");
                ex.Source = "Lucir Class";
                throw ex;
            }
        }

        public void Login(string u, string p)
        {
            this.Un = u;
            this.Pw = p;
            this.Login();

        }



    }
}
