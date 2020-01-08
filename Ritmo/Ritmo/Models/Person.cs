
namespace Ritmo
{
    public abstract class Person
    {
        public bool Loggedin { get; set; }
        public AccessLevel Access { get; set; }
        public int ConsumerID { get; set; }

        // create user after login, this user can be used to check the acces to other classes. 
        public Person(bool loggedin, AccessLevel access, int personID)
        {
            Loggedin = loggedin;
            Access = access;
            ConsumerID = personID;
        }
    }
}
