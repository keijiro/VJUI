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

The VJUI package uses the [scoped registry] feature to import dependent
packages. Please add the following sections to the package manifest file
(`Packages/manifest.json`).

To the `scopedRegistries` section:

```
{
  "name": "Keijiro",
  "url": "https://registry.npmjs.com",
  "scopes": [ "jp.keijiro" ]
}
```

To the `dependencies` section:

```
"jp.keijiro.klak.vjui": "1.0.2"
```

After changes, the manifest file should look like below:

```
{
  "scopedRegistries": [
    {
      "name": "Keijiro",
      "url": "https://registry.npmjs.com",
      "scopes": [ "jp.keijiro" ]
    }
  ],
  "dependencies": {
    "jp.keijiro.klak.vjui": "1.0.2",
...
```

[scoped registry]: https://docs.unity3d.com/Manual/upm-scoped.html
