using File_ChangeName;

var root = @"C:\Users\user\source\repos\projects\ScriptureExercise\Test\ScriptureExercise\ScriptureExercise\wwwroot\lib\DB\";
var changeNameHelper = new ChangeNameHelper
{
    SourcePath = root + "論語二",
    DestPath = root + "F",
    DestFilePrefix = "F2",
};

changeNameHelper.MoveFiles();