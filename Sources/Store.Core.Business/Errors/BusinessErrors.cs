namespace Store.Core.Business.Errors;

public static class BusinessErrors
{
    public static async Task<T> EnsureIsNotNull<T>(this Task<T?> item, string id)
        => (await item).EnsureIsNotNull(id);

    public static T EnsureIsNotNull<T>(this T? item, string id) 
        => item ?? throw new BusinessException(BusinessError.NotFound("not_found", id));
}