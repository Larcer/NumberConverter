# NumberConverter
Програма конвертує арабські числа в римські числа.
Для користування застосунком необхідно авторизуватись. Програма надає можливість перегляду попередніх запитів конвертації.
В програмі є обробка помилок. 

Результати ручного тестування:
1. Вікно авторизації
* Користувач не може натиснути кнопку "log in" поки не не введе значення полів login, password.
* Якщо ввести значення логіна, яке не було зареєстровано, після натиснення кнопки "log in", то користувачу буде відображено повідомлення, що користувача з таким логіном не існує.
* Якщо ввести зареєсстрований логін, але ввести некорректний пароль, то користувачу буде відображено повідомлення, що пароль не є вірним.
* Якщо ввести зареєстрований логін та корректний пароль, то користувачу буде відображено повідомлення про успішну авторизацію.
2. Вікно реєстрації
* Користувач не може натиснути кнопку "Sign up" поки не введе значення всіх полів форми.
* Користувач не може зареєструвати логін, якщо логін попередньо вже був зареєстрований. Користувачу буде відображено повідомлення про це.
* Користувач не може зареєструвати логін, якщо інший логін з таким же email попередньо вже був зареєстрований. Користувачу буде відображено повідомлення про це.
* Якщо поле "email" не має символу "@" та символів до і після цього символу, то користувачу буде відображено повідомлення, що email не є корректним.
3. Вікно застосунку
* Якщо ввести в поле "Enter arabic number" будь-який символ, що не є цифрою, окрім "+" на початку, то користувачу буде відображено повідомлення, щоб він ввів додатнє ціле арабське число.
* Якщо ввести в поле "Enter arabic number" цифру більшу за максимальне значення, то користувачу буде відображено повідомлення, щоб він ввів значення більше 0 та менше за максимальне.

Під час тестування програми багів не виявлено.