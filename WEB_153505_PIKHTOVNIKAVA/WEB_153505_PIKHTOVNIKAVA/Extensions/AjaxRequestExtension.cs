﻿namespace WEB_153505_PIKHTOVNIKAVA.Extensions
{
    public static class AjaxRequestExtension
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}
