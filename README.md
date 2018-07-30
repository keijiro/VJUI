VJUI
====

**VJUI** is a collection of custom UI controls specially designed for VJing.

![gif](http://66.media.tumblr.com/79e789d00361cd5002d36dcc394aaaa7/tumblr_og2kkyoNgI1qio469o1_320.gif)

It contains three custom controls:

**Knob** allows users to select a numeric value by dragging. This provides
almost the same functionality as the standard Slider control but a bit easier
to interact on a touch-sensitive screen.

![gif](http://66.media.tumblr.com/d094fed9d1ae7f7cf1d8de5e1ce4da74/tumblr_og62oqbDjZ1qio469o1_320.gif)

**Button** is almost identical to the standard Button control, but it also
provides the `OnButtonDown` event not only the `OnButtonUp` event. This is
convenient for VJing because button-down events are more often used than
button-ups.

**Toggle** is also nearly identical to the standard Toggle control but in the
same look-and-feel to the Knob and Button controls.

Installation
------------

VJUI is implemented as a package for [Unity Package Manager]. To install the
package into your project, copy `Packages/jp.keijiro.klak.vjui` from this
example project into `Packages` of your project.

Also you can directly clone the `upm` branch of this repository into the
`Packages` directory. This is convenient when using the [Git submodule] feature
to manage the package.

[Unity Package Manager]: https://docs.unity3d.com/Packages/com.unity.package-manager-ui@latest/
[Git submodule]: https://git-scm.com/book/en/v2/Git-Tools-Submodules

License
-------

Public domain
