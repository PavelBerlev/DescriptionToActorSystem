# 📦 ActorSystem

> Пример нативной реализации акторной модели в среде .NET

---

## 📑 Содержание

- [О проекте](#о-проекте)
- [Установка](#установка)
- [Использование](#использование)
---

## О проекте

BPMN (Business Process Model and Notation) — система условных обозначений для моделирования бизнес-процессов. Так как система подчиняется строгой формализации, значит ее можно представить в виде формального языка. Цель проекта - перевод формального описания процесса в цепочку акторов.
![Bpmn](https://cdn.statically.io/gh/bpmn-io/bpmn.io/99d5ea0/src/assets/img/toolkit/bpmn-js.gif)

- 🚀 **Быстрый** запуск
- 🔒 **Безопасная** архитектура
- 🧩 **Модульный** код  
  <br>

## Установка

```bash
git clone https://github.com/PavelKriko/DescriptionToActorSystem.git
cd ActorSystem/src/ActorSystem
docker compose up -d
dotnet restore          
dotnet build -c Release 
dotnet run  -c Release  
```

## Использование

По умолчанию ASP.NET откроет:

https://localhost:5001


