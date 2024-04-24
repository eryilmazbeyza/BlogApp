
namespace Blog.Application.Common.Security;

/// <summary>
/// commandlarda allowAnonymous varsa bu çalışıyor
/// .net kendi allowanonymous token var mı diye kontrol ediyor
/// tokenın içeriğini kontrol etmek için bunu oluşturduk
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class AllowAnonymousAttribute : Attribute
{
    public AllowAnonymousAttribute()
    {
    }

}