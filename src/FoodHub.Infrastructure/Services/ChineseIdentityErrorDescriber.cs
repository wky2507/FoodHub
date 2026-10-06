using Microsoft.AspNetCore.Identity;

namespace FoodHub.Infrastructure.Services // 根据你的项目命名空间调整
{
    public class ChineseIdentityErrorDescriber : IdentityErrorDescriber
    {
        // ------------------ 密码相关错误 ------------------
        public override IdentityError PasswordTooShort(int length)
            => new() { Code = nameof(PasswordTooShort), Description = $"密码长度至少为 {length} 位。" };

        public override IdentityError PasswordRequiresNonAlphanumeric()
            => new() { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "密码必须包含至少一个特殊字符（如 @,#,$ 等）。" };

        public override IdentityError PasswordRequiresDigit()
            => new() { Code = nameof(PasswordRequiresDigit), Description = "密码必须包含至少一个数字 ('0'-'9')。" };

        public override IdentityError PasswordRequiresLower()
            => new() { Code = nameof(PasswordRequiresLower), Description = "密码必须包含至少一个小写字母 ('a'-'z')。" };

        public override IdentityError PasswordRequiresUpper()
            => new() { Code = nameof(PasswordRequiresUpper), Description = "密码必须包含至少一个大写字母 ('A'-'Z')。" };

        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
            => new() { Code = nameof(PasswordRequiresUniqueChars), Description = $"密码必须包含至少 {uniqueChars} 个不同的字符。" };

        // ------------------ 用户名 / 邮箱相关错误 ------------------
        public override IdentityError DuplicateUserName(string userName)
            => new() { Code = nameof(DuplicateUserName), Description = $"用户名 '{userName}' 已被占用。" };

        public override IdentityError DuplicateEmail(string email)
            => new() { Code = nameof(DuplicateEmail), Description = $"邮箱 '{email}' 已被注册。" };

        public override IdentityError InvalidUserName(string? userName)
            => new() { Code = nameof(InvalidUserName), Description = $"用户名 '{userName}' 无效，只能包含字母或数字。" };

        public override IdentityError InvalidEmail(string? email)
            => new() { Code = nameof(InvalidEmail), Description = $"邮箱地址 '{email}' 格式不正确。" };

        // ------------------ 账号状态 / Token 相关错误 ------------------
        public override IdentityError DefaultError()
            => new() { Code = nameof(DefaultError), Description = "发生未知错误，请重试。" };

        public override IdentityError ConcurrencyFailure()
            => new() { Code = nameof(ConcurrencyFailure), Description = "数据并发冲突，修改失败。" };

        public override IdentityError InvalidToken()
            => new() { Code = nameof(InvalidToken), Description = "验证码或 Token 已失效/不正确。" };

        public override IdentityError LoginAlreadyAssociated()
            => new() { Code = nameof(LoginAlreadyAssociated), Description = "该第三方账号已绑定其他用户。" };

        public override IdentityError UserAlreadyHasPassword()
            => new() { Code = nameof(UserAlreadyHasPassword), Description = "该用户已设置过密码。" };

        public override IdentityError UserLockoutNotEnabled()
            => new() { Code = nameof(UserLockoutNotEnabled), Description = "此账号未开启锁定功能。" };
    }
}