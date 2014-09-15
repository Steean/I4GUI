/**
 * Created by StianFredsted on 14-01-14.
 */
function TodoController($scope) {
    $scope.todos = [];

    var tempTodos = [];

    $scope.addTask = function () {
        if($scope.TaskTitle == null || $scope.TaskTitle == "")
        {
            alert("You have not entered a title");
            return;
        }

        if($scope.TaskDueDate == null || $scope.TaskDueDate == "")
        {
            alert("You have not entered a due date");
            return;
        }

        $scope.todos.push({
            title:$scope.TaskTitle,
            description:$scope.TaskDescription,
            priority:$scope.TaskPriority,
            dueDate:$scope.TaskDueDate,
            done:false});
        $scope.TaskTitle = '';
        $scope.TaskDescription = '';
        $scope.TaskPriority = '';
        $scope.TaskDueDate = '';
    };

    $scope.allTasks = function () {
        $scope.todos = tempTodos;
    };

    $scope.todoTasks = function () {
        if(tempTodos.length == 0)
        {
            tempTodos = $scope.todos;
        }
        $scope.todos = $scope.todos.filter(function(todo){
            return !todo.done;
        });
    };

    $scope.todayTasks = function () {
        if(tempTodos.length == 0)
        {
            tempTodos = $scope.todos;
        }
        $scope.todos = $scope.todos.filter(function(todo){
            return todo.dueDate
        });
    };

    $scope.completedTasks = function () {
        if(tempTodos.length == 0)
        {
            tempTodos = $scope.todos;
        }
        $scope.todos = $scope.todos.filter(function(todo){
            return todo.done;
        });
    };

    $scope.current = "";

    $scope.currentTask = function (task) {
        $scope.current = task;
        $scope.SelectedTaskTitle = task.title;
        $scope.SelectedTaskDescription = task.description;
        $scope.SelectedTaskPriority = task.priority;
        $scope.SelectedTaskDueDate = task.dueDate;
    }

    $scope.updateTask = function() {
        if($scope.SelectedTaskTitle == null || $scope.SelectedTaskTitle == "")
        {
            alert("You have not entered a title");
            return;
        }

        if($scope.SelectedTaskDueDate == null || $scope.SelectedTaskDueDate == "")
        {
            alert("You have not entered a due date");
            return;
        }
        angular.forEach($scope.todos, function(todo) {
             if(todo.title == $scope.current.title)
             {
                 todo.title = $scope.SelectedTaskTitle;
                 todo.description = $scope.SelectedTaskDescription;
                 todo.priority = $scope.SelectedTaskPriority;
                 todo.dueDate = $scope.SelectedTaskDueDate;
             }
        })
    }

    $scope.deleteTask = function () {
        for(var i = 0; i < $scope.todos.length; i++){
            if($scope.todos[i].title == $scope.current.title){
                $scope.todos.splice(i,1);
            }
        }

        var temp = "";
        $scope.currentTask(temp);
    }
}


