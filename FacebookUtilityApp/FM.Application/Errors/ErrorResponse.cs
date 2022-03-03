﻿namespace FM.Application.Errors
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {

        }

        public ErrorResponse(ErrorModel error)
        {
            Errors.Add(error);
        }

        public List<ErrorModel> Errors { get; set; } = new();
    }
}