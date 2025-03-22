using System.Net;

namespace Arale.CodeGen.Models;

/// <summary>
///     Common api response
/// </summary>
public class ApiResult<T>
{
    private const int SuccessCode = 0;
    private const string SuccessMsg = "success";
    private const int DefaultFailCode = (int)HttpStatusCode.InternalServerError;
    private const string DefaultFailMsg = "Unknow Server Error, please try again later:(";

    /// <summary>
    ///     Status code, 0 = success, other = fail
    /// </summary>
    public int Code { get; set; } = SuccessCode;

    /// <summary>
    ///     Message
    /// </summary>
    public string Msg { get; set; } = SuccessMsg;

    /// <summary>
    ///     Response timestamp
    /// </summary>
    public long Timestamp { get; set; } = DateTimeOffset.Now.ToUnixTimeMilliseconds();

    /// <summary>
    ///     Data
    /// </summary>
    public T? Data { get; set; }

    public static ApiResult<T> Success(T? data)
    {
        return new ApiResult<T>
        {
            Code = SuccessCode,
            Msg = SuccessMsg,
            Data = data
        };
    }

    public static ApiResult<T> Fail(string? msg = DefaultFailMsg, int code = DefaultFailCode)
    {
        return new ApiResult<T>
        {
            Code = code,
            Msg = msg ?? DefaultFailMsg
        };
    }
}
