namespace BNPL_Web.Helpers
{
    public static class Error
    {
        public static string AccessDenied { get; } = "Access Denied!";
        public static string InvalidToken { get; } = "Invalid Token!";
        public static string MissingAuthorization { get; } = "Please login to use this functionality.";
        public static string TokenExpired { get; } = "Access Denied. Token Expired!";
        public static string AccoutNotVerified { get; } = "Please verify your account first.";
        public static string AccoutNotApproved { get; } = "Sorry! Your account is not approved yet.";
        public static string ServerError { get; } = "Something went wrong.";
        public static string PhoneAlreadyExists { get; } = "Phone Number already exists!";
        public static string AlreadyLiked { get; } = "Already liked!";
        public static string AlreadyExists { get; } = "Already Exists!";
        public static string AlreadyRated { get; } = "Already rated!";
        public static string PhoneDoesnotExists { get; } = "Your entered phone number doesn't exist";
        public static string WrongPassword { get; } = "Wrong password!";
        public static string WrongEmail { get; } = "Wrong email!";
        public static string UserDoesnotExists { get; } = "User doesn’t exist.";
        public static string FileNotFound { get; } = "File not found!";
        public static string LoginFailed { get; } = "Login Failed!";
        public static string AccountBlocked { get; } = "Your account has been blocked!";
        public static string InvalidCode { get; } = "Your verification code is incorrect, Please re-enter your 4 digit code.";
        public static string DuplicateCode { get; } = "Duplicate Code!";
        public static string InvalidId { get; } = "Invalid Id!";
        public static string EmailAlreadyExist { get; } = "Email Already Exist!";
    }
}
