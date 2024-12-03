Feature: TodoItem
  As a user,
  I want to manage todo items,
  So that I can organize my work.

  Scenario: NewTodoItem
    Given the user is on the home page and clicks the New TodoItem button
    When the user provides valid new todo item details and clicks the Save button
      | TaskName   | Notes                                | Done  |
      | Yakety Yak | Take out the papers and the trash    | false |
    Then the user should be taken to the home page and see the new TodoItem