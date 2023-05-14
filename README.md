VJUI
====

**VJUI** is a collection of custom UI controls specially designed for VJing.

![gif](https://i.imgur.com/COKYrxA.gif)

It contains three custom controls:

**Knob** allows users to select a numeric value by dragging. This provides
almost the same functionality as the standard Slider control but a bit easier
to interact on a touch-sensitive screen.

![gif](https://i.imgur.com/nktaIxg.gif)

**Button** is almost identical to the standard Button control, but it also
provides the `OnButtonDown` event not only the `OnButtonUp` event. This is
convenient for VJing because button-down events are more often used than
button-ups.

**Toggle** is also nearly identical to the standard Toggle control but in the
same look-and-feel to the Knob and Button controls.

How To Install
--------------

This package uses the [scoped registry] feature to resolve package
dependencies. Open the Package Manager page in the Project Settings window and
add the following entry to the Scoped Registries list:

- Name: `Keijiro`
- URL: `https://registry.npmjs.com`
- Scope: `jp.keijiro`

![Scoped Registry](https://user-images.githubusercontent.com/343936/162576797-ae39ee00-cb40-4312-aacd-3247077e7fa1.png)

Now you can install the package from My Registries page in the Package Manager
window.

![My Registries](https://user-images.githubusercontent.com/343936/162576825-4a9a443d-62f9-48d3-8a82-a3e80b486f04.png)

[scoped registry]: https://docs.unity3d.com/Manual/upm-scoped.html
