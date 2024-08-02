using System;
using System.Collections.Generic;
using System.Text;
using QuizPlatform.Models;

namespace QuizPlatform.Infrastructure
{
    public interface IDbFactory
    {

        QuizPlatformContext Initialize();

    }
}
