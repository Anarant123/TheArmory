namespace TheArmory.Domain.Models.Message.Errors
{
    public class ErrorsMessage
    {
        public static string ErrorSavingChanges => "Ошибка при сохранении данных";

        public static string UserNotFound => "Пользователь не найден";
    
        public static string AdminNotFound => "Админ не найден";
    
        public static string SomethingWentWrong => "Что-то пошло не так";

        public static string ConfirmPasswordNotMatch => "Пароли не совпадают";

        public static string InvalidPassword => "Неверный пароль";
    
        public static string InvalidEmail => "Неверная почта";

        public static string InaccessibleLogin => "Недоступный логин";

        public static string InaccessiblePhoneNumber => "Недоступный номер телефона";

        public static string NoAccess => "Нет доступа";
    }
}