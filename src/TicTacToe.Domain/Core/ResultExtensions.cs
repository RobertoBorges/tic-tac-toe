﻿using System;

namespace TicTacToe.Domain.Core
{
    public static class ResultExtensions
    {
        public static Result OnSuccess(this Result result, Func<Result> func)
        {
            if (result.Failure)
                return result;

            return func();
        }

        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.Failure)
                return result;

            action();

            return Result.Ok();
        }

        public static Result OnSuccess<T>(this Result<T> result, Action<T> action)
        {
            if (result.Failure)
                return result;

            action(result.Value);

            return Result.Ok();
        }

        public static Result<T> OnSuccess<T>(this Result result, Func<T> func)
        {
            if (result.Failure)
                return Result.Fail<T>(result.Error);

            return Result.Ok(func());
        }

        public static Result<TDestination> OnSuccess<TSource, TDestination>(this Result<TSource> result, Func<TSource, Result<TDestination>> func)
        {
            if (result.Failure)
                return Result.Fail<TDestination>(result.Error);
            var innerResult = func(result.Value);
            if (innerResult.Failure)
                return Result.Fail<TDestination>(innerResult.Error);
            return Result.Ok(innerResult.Value);
        }
        public static Result<TDestination> OnSuccess<TSource, TDestination>(this Result<TSource> result, Func<TSource, TDestination> func)
        {
            if (result.Failure)
                return Result.Fail<TDestination>(result.Error);
           
            return Result.Ok(func(result.Value));
        }

        public static Result<T> OnSuccess<T>(this Result result, Func<Result<T>> func)
        {
            if (result.Failure)
                return Result.Fail<T>(result.Error);

            return func();
        }

        public static Result OnSuccess<T>(this Result<T> result, Func<T, Result> func)
        {
            if (result.Failure)
                return result;

            return func(result.Value);
        }

        public static Result OnFailure(this Result result, Action action)
        {
            if (result.Failure)
            {
                action();
            }

            return result;
        }
        public static Result OnFailure(this Result result, Action<string> action)
        {
            if (result.Failure)
            {
                action(result.Error);
            }

            return result;
        }
        public static Result OnBoth(this Result result, Action<Result> action)
        {
            action(result);

            return result;
        }

        public static T OnBoth<T>(this Result result, Func<Result, T> func)
        {
            return func(result);
        }
    }
}
