# SVG Structure and Attribute Convention for Color Application [Version 1.0]

This document outlines the conventions for structuring SVG mockups and defining attributes to facilitate color application in the mockup tool.

## 1. Identifiable Colorable Elements

Identify elements within SVG mockups that will be colorable. These typically include:

- Backgrounds
- Text
- Icons
- Buttons

## 2. Attribute Naming Convention

Use a custom data attribute named `data-color-role` to tag colorable elements in SVG mockups.

## 3. Defined Color Roles

Assign one of the following color roles to the `data-color-role` attribute based on the intended color application:

- `primary`: For primary backgrounds or elements.
- `secondary`: For secondary backgrounds or elements.
- `text`: For text elements.
- `accent`: For accent colors on buttons or icons.

## 4. Attribute Value Assignment

Assign the corresponding color role value to the `data-color-role` attribute on each colorable element within SVG mockups.
The value should correspond to the intended color application for that element.

Example:
```xml
<svg xmlns="http://www.w3.org/2000/svg">
    <rect width="100" height="100" data-color-role="primary"/>
    <text x="10" y="40" data-color-role="text">Hello, World!</text>
    <circle cx="50" cy="50" r="10" data-color-role="accent"/>
</svg>
