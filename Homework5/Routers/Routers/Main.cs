using Routers;

if (args[0] != null && args[1] != null)
{
    var routersNetwork = new RoutersNetwork(@args[0]);
    routersNetwork.BuildNetwork(@args[1]);
}
