namespace Pizza.Utilities
{
    public class Message
    {
        public const string Registering_Message = "Congratulations on successfully registering for our pizza application! 🎉🍕 \n\nUser-ID: ";
        public const string Login_Message = "Successful login to pizza application. We are providing you with a JWT Token for secure access to our platform.\n\nToken: ";
        public const string Forget_Password = "Password: ";
        public const string Manage_Order = "Order status updated successfully";
        public const string Order_Placed = "Order placed successfully, Amount to pay: ";

        //For Server side error
        public const string Server_Error = "Oops! We apologize for the inconvenience, but it seems that there is an issue with our server at the moment. Please try again later";

        //Messages for custome exceptions.
        public const string User_Already_Exist = "We are sorry, but it seems that you already have an account with us. If you have forgotten your login details, please use the 'Forgot Password' option to recover your account.";
        public const string Incorrect_EmailOrPassword = "We are sorry, but the email or password you provided is incorrect. Please make sure you have entered the correct information and try again.";
        public const string UserNotFound_ForLogin = "Sorry, we could not find an account associated with the provided email address. Please make sure you have entered the correct email or sign up if you haven't created an account yet.";
        public const string UserNotFound_ForForgetPassword = "Sorry, we could not find an account associated with the provided email address or user id. Please make sure you have entered the correct email and user id.";
        public const string OrderNotFound = "Order Not Found With Given Order_Id: ";
        public const string PizzaNotFound = "Sorry, we could not find any Pizza with the provided Pizza-Id";
        public const string ToppingNotFound = "Sorry, we could not find any Topping with the provided Pizza-Id";
    }
}
