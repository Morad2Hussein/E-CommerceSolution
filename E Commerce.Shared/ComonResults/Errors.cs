
namespace E_Commerce.Shared.ComonResults
{
    public class Errors
    {
        public string Code { get; }
        public string Description { get; }
        public ErrorTypes Types { get; }

        public Errors(string code, string description, ErrorTypes types)
        {
            Code = code;
            Description = description;
            Types = types;
        }
        #region Factory Functions 

        #region failure Functions 
        public static Errors Failure(string code = "General.Failure", string description = "A General Failure Has Occurred")
        {
            return new Errors(code, description, ErrorTypes.Failure);
        }
        #endregion
        #region Vaildation 
        public static Errors Vaildation(string code = "General.Vaildation", string description = "One Or More Vaildation Errors Occurred")
        {
            return new Errors(code, description, ErrorTypes.Vaildation);
        }
        #endregion

        // InValidCredentials= 5

        #region Not Found
        public static Errors NotFound(string code = "General.NotFound", string description = "The Requested Resource Was Not Found")
        {
            return new Errors(code, description, ErrorTypes.NotFound);
        }
        #endregion
        #region Unauthorized
        public static Errors Unauthorized(string code = "General.Unauthorized", string description = "You Are Not Authorized To Access This Resource")
        {
            return new Errors(code, description, ErrorTypes.Unauthorized);
        }
        #endregion
        #region  Forbidden
        public static Errors Forbidden(string code = "General.Forbidden", string description = "Access To This Resource Is Forbidden")
        {
            return new Errors(code, description, ErrorTypes.Forbidden);
        }

        #endregion
        #region InValidCredentials
        public static Errors InValidCredentials(string code = "General.InValidCredentials", string description = "The Provided Credentials Are Invalid")
        {
            return new Errors(code, description, ErrorTypes.InValidCredentials);
        }
        #endregion

        #endregion










    }
}
