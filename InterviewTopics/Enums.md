# Enums in C#

## What is an Enum?
An `enum` (short for *enumeration*) is a value type in C# used to declare a set of named constants.

### Syntax:
```csharp
enum Days { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday };

### Can a Class Derive from an Enum in C#?
- The classes cannot be derived from enums. This is because Enums are treated as sealed classes and hence all rules that are applicable to sealed classes also apply to enums. Sealed means a class can not further take part in inheritance.
