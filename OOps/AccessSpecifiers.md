# Access Specifiers in C#

## What are Access Specifiers?

Access Specifiers in C# are special types of **modifiers** used to define the **scope** or **accessibility** of types and their members. This means they determine **who can access** a particular class, method, variable, property, or any other member in your program.

## Modifiers Overview

C# provides several **modifiers** that can be applied to types and type members, such as:
- `private`
- `public`
- `protected`
- `internal`
- `protected internal`
- `private protected`
- `static`
- `abstract`
- `sealed`
- `virtual`
- `partial`
- `base`

> ⚠️ Note: While all access specifiers are modifiers, not all modifiers are access specifiers.

## Purpose of Access Specifiers

Access specifiers are mainly used to define:
- The **scope of types** (such as `class`, `struct`, `interface`, `enum`, `delegate`)
- The **scope of type members** (such as `fields`, `methods`, `properties`, `constructors`)

The term **scope** here refers to **accessibility or visibility** — i.e., *who* can access the member and *who* cannot.

For example, if you define a class with several methods and variables, access specifiers determine which of those can be accessed from outside the class and which cannot.

---

## Common Access Specifiers

| Access Specifier     | Accessibility                                                                 |
|----------------------|--------------------------------------------------------------------------------|
| `public`             | Accessible from any other class or assembly                                   |
| `private`            | Accessible only within the containing class                                   |
| `protected`          | Accessible within the containing class and its derived classes                |
| `internal`           | Accessible within the same assembly, but not from another assembly            |
| `protected internal` | Accessible within the same assembly or from derived classes in another assembly |
| `private protected`  | Accessible within the containing class or derived classes in the same assembly |

---

## Summary

In simple terms:

- Access Specifiers define **"who can use"** what.
- They are crucial for maintaining **encapsulation**, **security**, and **clarity** in your code.
- Use them wisely to avoid **exposing internal logic** to the outside world unintentionally.

## Types and Type Members

- In C#, **classes**, **structs**, **enums**, **interfaces**, and **delegates** are called **types**.
- **Variables**, **properties**, **constructors**, **methods**, etc., that reside inside a type are called **type members**.

### Important Rules:
- **Type Members** can use **all six** access specifiers.
- **Types** (non-nested) can only use:
  - `internal`
  - `public`

> ⚠️ By default:
> - A **type** is `internal` if no access specifier is mentioned.
> - A **type member** is `private` if no access specifier is specified.

---

## Types of Access Specifiers in C#

C# supports **six** access specifiers:

---

---

## Access Specifiers and Member Accessibility Scope

Access Specifiers (or Modifiers) define **where** type members can be accessed from. To fully understand their usage, it's important to identify the **different scopes** where a member might be accessed.

### 📌 Scopes for Type Members:

1. **Within the Class**  
2. **Derived Class in the Same Assembly**
3. **Non-Derived Class in the Same Assembly**
4. **Derived Class in Other Assemblies**
5. **Non-Derived Class in Other Assemblies**

---

### 🔍 Access Specifiers vs. Scope Matrix

| Access Specifier     | Within Class | Derived Class (Same Assembly) | Non-Derived Class (Same Assembly) | Derived Class (Other Assembly) | Non-Derived Class (Other Assembly) |
|----------------------|--------------|-------------------------------|-----------------------------------|-------------------------------|-------------------------------------|
| `private`            | ✅           | ❌                            | ❌                                | ❌                            | ❌                                  |
| `public`             | ✅           | ✅                            | ✅                                | ✅                            | ✅                                  |
| `protected`          | ✅           | ✅                            | ❌                                | ✅                            | ❌                                  |
| `internal`           | ✅           | ✅                            | ✅                                | ❌                            | ❌                                  |
| `protected internal` | ✅           | ✅                            | ✅                                | ✅                            | ❌                                  |
| `private protected`  | ✅           | ✅                            | ❌                                | ❌                            | ❌                                  |

---

> 🧠 **Note:**  
> The restrictions come into play **only when accessing the members outside the containing type**.  
> Inside the class itself, everything is accessible regardless of the access specifier.

---

### ✅ Use Case Reminder

- Use `private` when you want to **strictly encapsulate** functionality within the same class.
- Use `protected` for **inheritance-based extension** within the same or different assemblies.
- Use `internal` for **intra-assembly communication**.
- Use `protected internal` for **inheritance + assembly access**.
- Use `private protected` to **limit to derived classes but only in the same assembly**.
- Use `public` only when the member **must be exposed globally**.

---

#### Private 

- With the Class: YES
- Derived Class in Same Assembly: NO
- Non-Derived Class in Same Assembly: NO
- Derived Class in Other Assemblies: NO
- Non-Derived Class in Other Assemblies: NO

#### Public 

- With the Class: YES
- Derived Class in Same Assembly: YES
- Non-Derived Class in Same Assembly: YES
- Derived Class in Other Assemblies: YES
- Non-Derived Class in Other Assemblies: YES


#### Protected

- With the Class: YES
- Derived Class in Same Assembly: YES
- Non-Derived Class in Same Assembly: NO
- Derived Class in Other Assemblies: YES
- Non-Derived Class in Other Assemblies: NO

#### Internal

- With the Class: YES
- Derived Class in Same Assembly: YES
- Non-Derived Class in Same Assembly: YES
- Derived Class in Other Assemblies: NO
- Non-Derived Class in Other Assemblies: NO

#### Protected Internal

- With the Class: YES
- Derived Class in Same Assembly: YES
- Non-Derived Class in Same Assembly: YES
- Derived Class in Other Assemblies: YES
- Non-Derived Class in Other Assemblies: NO

#### Private Protected

- With the Class: YES
- Derived Class in Same Assembly: YES
- Non-Derived Class in Same Assembly: NO
- Derived Class in Other Assemblies: NO
- Non-Derived Class in Other Assemblies: NO


## Access Specifiers with Type in C#:

- Note: The point that you need to remember is if you want to access the class only within the same assembly, then you need to declare the class as internal and if you want to access the class from the same assembly as well as from other assemblies then you need to declare the class as public.

