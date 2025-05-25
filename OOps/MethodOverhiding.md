
# Method Overhiding in C# (by Saurabh Shukla)

## ✅ Topics Covered:
- Method Overriding
- Method Hiding
- Function Overloading
- Early vs Late Binding
- Real-life Examples
- Key Differences
- Behavior with Reference Types & Pointers

---

## 1. 🔁 Method Overriding

- Occurs when a child class **redefines a method** from the parent class with **the same signature**.
- Enables **runtime polymorphism** (late binding).
- Requires the `virtual` keyword in base class and `override` in derived class.

### ✅ Example:

```csharp
class A {
    public virtual void f1() {
        Console.WriteLine("A::f1");
    }

    public void f2() {
        Console.WriteLine("A::f2");
    }
}

class B : A {
    public override void f1() {
        Console.WriteLine("B::f1 (Overridden)");
    }

    public void f2(int x) {
        Console.WriteLine($"B::f2 with {x} (Hidden)");
    }
}
```

---

## 2. 🧱 Method Hiding

- Happens when a method in the derived class **has the same name** as one in the base class, but with a **different signature**.
- The method in the base class becomes **inaccessible** if the name is matched.
- Use `new` keyword (optional but recommended for clarity).

### 🚫 Example - No `new` keyword:

```csharp
class B : A {
    public void f2(int x) {
        Console.WriteLine("Hidden method in B");
    }
}
```

> ⚠️ This **hides** `A.f2()`, but doesn't override it.

---

## 3. 📌 Behavior in Main Method

### Example:

```csharp
static void Main(string[] args) {
    B obj = new B();

    obj.f1();     // Output: B::f1 (Overridden)
    obj.f2(4);    // Output: B::f2 with 4
    obj.f2();     // ❌ Compile-time error: No method f2() in class B
}
```

### 📌 Why `obj.f2()` fails:

- `obj` is of type `B`.
- Compiler searches for `f2()` in `B`.
- It finds `f2(int x)` and **stops search** (early binding).
- Since `f2()` (no args) is not found in `B`, it throws an **error**, even though `A.f2()` exists.

---

## 4. 💡 Overloading vs Overriding vs Hiding

| Concept         | Same Signature | Same Name | Different Signature | Same Class | Inheritance Required | Keyword       |
|----------------|----------------|-----------|----------------------|------------|----------------------|----------------|
| Overloading     | ❌             | ✅        | ✅                   | ✅         | ❌                   | None           |
| Overriding      | ✅             | ✅        | ❌                   | ❌         | ✅                   | `virtual` + `override` |
| Hiding          | ❌/✅          | ✅        | ✅/❌                | ❌         | ✅                   | `new` (optional) |

---

## 5. 📍 Method Binding with Reference Types (Early vs Late Binding)

### 🔍 Example:

```csharp
class A {
    public virtual void f1() {
        Console.WriteLine("A::f1");
    }
}

class B : A {
    public override void f1() {
        Console.WriteLine("B::f1 (Overridden)");
    }

    public void f2() {
        Console.WriteLine("B::f2");
    }
}

static void Main(string[] args) {
    A p;           // Reference of type A
    B o2 = new B();
    p = o2;        // Reference pointing to object of B

    p.f1();        // Output: B::f1 (Late binding - resolves at runtime)
}
```

### 🔁 Explanation:

- `p` is of type `A`, but points to an object of type `B`.
- `f1()` is marked as `virtual`, so **late binding** occurs.
- Actual method called is from class `B`, not `A`.

---

## 6. 🔧 What if `f1()` is not virtual?

```csharp
class A {
    public void f1() {
        Console.WriteLine("A::f1");
    }
}

class B : A {
    public new void f1() {
        Console.WriteLine("B::f1 (Hidden)");
    }
}

static void Main(string[] args) {
    A p;
    B o2 = new B();
    p = o2;

    p.f1();  // Output: A::f1 (Early binding)
}
```

### 🔍 Why this happens:

- `f1()` in `A` is not virtual.
- So, **binding is compile-time** (early).
- Compiler uses reference type (`A`) → Calls `A.f1()` even though object is `B`.

---

## 7. 🏎️ Real-World Analogy

- `Car` → `StartEngine()`: Basic functionality.
- `SuperCar` → `StartEngine()`: Overrides for turbo boost.
- If `StartEngine()` is virtual, calling via base class reference will still use `SuperCar` version (late binding).

---

## 8. 🧠 Summary: When to Use What

| Use Case                           | Use This        |
|------------------------------------|-----------------|
| Modify parent method's behavior    | Method Overriding (`virtual` + `override`) |
| Add method with same name & diff args | Method Hiding (`new`) |
| Multiple methods with same name in same class | Overloading       |
| Base class ref calling child method at runtime | Make it `virtual`  |

---

## 📌 Final Notes

- Always use `virtual` in base and `override` in child to ensure proper overriding.
- Use `new` if you intentionally want to **hide** the base method.
- Avoid hiding unless necessary – it can lead to confusion and maintenance issues.






class A {
    public void f1() {};
    public void f2() {};
}

class B : A {
    void f1() {};  // Method overriding
    void f2(int x) {} // Method hiding
}

void main() {
    B obj;
    obj.f1() // so in this what will happen compiler will go and the type of the object from which function is called so the type is B so it will directly go to Class B and call the function f1.

    obj.f2(); // now here what will happen again compiler we do the same early binding it will see the object type and object type is B it will go in class B and search for function f2 and it will find the function f2 but there is a rule if compiler will get the function name in the class it will not go further to search in parent for the exact match. Hence we do have function by the name f2 in Class B but we do not have without any input so it will throw error.

    obj.f2(4) // B 
    // If you will see here f2(4) is working fine but if both f2() and f2(4) would work we can simply say that I will be function overloading but function overloading happen inside same class only.

}

- So what is methodoverring, let say you got some function from the parent class and you want to change the functionality of that method in the child class then you should use method overriding.(Do not forget the car and super car example)


- Now let's do the same thing using the pointer

class A {
    public: 
    void f1() {};
}

class B : A {
    void f1() {}; // overriding
    void f2() {};
};

void main() {
    A *p,o1;
    B o2;
    p = &o2;
    p->f1(); // lets understand this this time also early binding will takes place now when compiler will reach to this point it will check for the type of p and it will find the reference for A but because p is a pointer and it has stored the address of B it should call f1 from B class but at the compile time no memory allocation has happened to it will take the reference of A only and call the function f1() from A and this is the issue that we want to call the function f1() from B but it will call from A and the main point of concern is that it will not give any error also;
    So what is the solution, solution is function binding should happen at the runtime and not at the compile time so p will have the reference of B and it will call the f1() from B.
}