var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async(HttpContext context) =>
{

    if(context.Request.Method=="GET" && context.Request.Path == "/")
    {

        int firstno = 0, secondno = 0;
        string? operation=null;
        long? result=null;

        //read first number
        if (context.Request.Query.ContainsKey("firstno"))
        {
            string firstnoString = context.Request.Query["firstno"][0];
            if(!string.IsNullOrEmpty(firstnoString))
            {
                firstno= int.Parse(firstnoString);
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid Input for first number\n");
            }
        }

        else
        {
            if(context.Response.StatusCode == 200)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("No Input for first number\n");

            }
        }

        //read second number
        if (context.Request.Query.ContainsKey("secondno"))
        {
            string secondnoString = context.Request.Query["secondno"][0];
            if (!string.IsNullOrEmpty(secondnoString))
            {
                secondno = int.Parse(secondnoString);
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid Input for second number\n");
            }
        }

        else
        {
            if (context.Response.StatusCode == 200)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("No Input for second number\n");

            }
        }

        //read operator
        if (context.Request.Query.ContainsKey("operation"))
        {
            operation = Convert.ToString(context.Request.Query["operation"][0]);

            //calculation
            switch (operation)
            {
                case "add":result=firstno+secondno; break;
                case "subtract":result= firstno-secondno; break;
                case "multiply":result= firstno*secondno; break;
                case "divide": result = (secondno != 0) ? firstno / secondno : 0; break;
                case "mod": result = (secondno != 0) ? firstno % secondno : 0; break;

            }

            if (result.HasValue)
            {
                await context.Response.WriteAsync($"Result of {operation} operation on {firstno.ToString()} & {secondno.ToString()} is: {result.Value.ToString()}");
            }

            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid Input for operation\n");
            }
        }
        else
        {
            if (context.Response.StatusCode == 200)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("No Input for Operation\n");
            }
        }


    }
});

app.Run();
