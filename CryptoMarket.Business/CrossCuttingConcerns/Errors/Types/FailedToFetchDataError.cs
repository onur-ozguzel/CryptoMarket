﻿using FluentResults;

namespace CryptoMarket.Business.CrossCuttingConcerns.Errors.Types
{
    public class FailedToFetchDataError : Error
    {
        public FailedToFetchDataError(int statusCode) : base($"Failed to fetch data from 3rd party API. Status code: {statusCode}")
        {
        }
    }
}