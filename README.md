# ToDoList
ToDoList for Intellishift

There are 3 projects:

ToDoListAPI - This is the API that has to run for the front end to work
ToDoListAPP - This is the user interface to test the API
ToDoListProjectTest - Will run a couple of simple unit tests on the API. No need to run the API project for running these tests.

=== List endpoint ===

Get ToDoList/{id}
Get ToDoList/All
Get ToDoList/AllAssociated/{id}  => id is ListId and all with same associate name will be returned
Post ToDoList
Delete ToDoList/{id}
Patch ToDoList   => content is ToDoList object

=== Item endpoint ===

Get ToDoListItem/List/{id}
Get ToDoListItem/{id}
Post ToDoListItem => Content is toDoListItem object
Patch ToDoListItem/{id}
Delete ToDoListItem/{id}
Patch ToDoListItem/MarkCompleted/{id}
