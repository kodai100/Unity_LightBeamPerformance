# Light Beam / Laser Performance Package for Unity

ムービングライト、レーザーの演出を、タイムラインでコントロール可能にするUnity用パッケージです。

![thumbnail](https://github.com/kodai100/Unity_LightBeamPerformance/blob/master/Thumbnails/thumbnail.gif)

![timeline](https://github.com/kodai100/Unity_LightBeamPerformance/blob/master/Thumbnails/timeline.png)

# Install With PackageManager

```
{
  "dependencies": {
    "com.kodai100.light-beam-performance": "https://github.com/ProjectBLUE-000/Unity_LightBeamPerformance.git?path=/Assets/LightBeamPerformance"
  }
}
```

# Usecase

- [KAGUYA LUNA LIVE 2nd](https://www.youtube.com/watch?v=yiVGudcnsAg)
- [KANO Music Video](https://www.youtube.com/watch?v=LS61xdsolis)
- [You Make Shibuya](https://www.youtube.com/watch?v=CJiCQdF8YJ8)
- [SHINKAI FES](https://www.youtube.com/watch?v=Bizxhyzdx78)
- [Splash LIVE](https://www.youtube.com/watch?v=9nSDP0KnUE8)

# Light Beam

## セットアップ方法

### ムービングライトの配置
Prefabsフォルダに含まれる、MovingLightプレファブ(以下、灯体)をシーンの好きな位置、好きな方向に配置します。

### ムービングライトをグループ化する
LightGroupスクリプトを新規GameObjectへアタッチし、先程追加した灯体を登録します。

![group](https://github.com/kodai100/Unity_LightBeamPerformance/blob/master/Thumbnails/group.png)

このオブジェクトの子階層に灯体のオブジェクトをまとめると便利です。

また、グループは複数作成することができ、グループごとに演出を切り分けることが出来ます。

### ムービングライト制御用コンポーネントを作成する
ムービングライトを制御するために、`LightBeamPerformance`スクリプトを新規GameObjectにアタッチします。

このコンポーネントに先程作成したグループを登録します。

![set-group](https://github.com/kodai100/Unity_LightBeamPerformance/blob/master/Thumbnails/set-group.png)

### Timelineにトラックを作成する
Timelineを新規作成した後、LightPerformanceトラックを追加します。
![track](https://github.com/kodai100/Unity_LightBeamPerformance/blob/master/Thumbnails/track.png)

トラックのバインディングに、LightBeamPerformanceコンポーネントを追加します。
![binding](https://github.com/kodai100/Unity_LightBeamPerformance/blob/master/Thumbnails/binding.png)

クリップを作成し、カーソルを動かしてクリップに載せると、クリップの情報を元にムービングライトが動くようになります。
![sample](https://github.com/kodai100/Unity_LightBeamPerformance/blob/master/Thumbnails/sample.png)

![sample2](https://github.com/kodai100/Unity_LightBeamPerformance/blob/master/Thumbnails/sample2.png)

グループごとにグラデーションをかけることが可能であったり、照明のアニメーションを分けることが可能です。
![gradient](https://github.com/kodai100/Unity_LightBeamPerformance/blob/master/Thumbnails/gradient.png)

## 備考
LightBeamPerformanceコンポーネントは、複数配置することが可能で、
上部や下部といった分割を行うことで、タイムライン上で演出を分けることが可能です。

![multi](https://github.com/kodai100/Unity_LightBeamPerformance/blob/master/Thumbnails/multi.png)

![multi-bottom](https://github.com/kodai100/Unity_LightBeamPerformance/blob/master/Thumbnails/multi_bottom.png)

# Laser
## セットアップ方法

レーザーのセットアップ方法も、ムービングライトのセットアップ方法と全く同一です。
Prefabsから、Laserプレファブを、灯体と同様に配置し、セットアップを行ってください。

![laser](https://github.com/kodai100/Unity_LightBeamPerformance/blob/master/Thumbnails/laser.png)

# Copyright of "Noriben Light Beam"
We used "Noriben Light Beam" shader in this project. 

Read the license in the following link.

[https://booth.pm/ja/items/1637978](https://booth.pm/ja/items/1637978)

In a nutshell, redistribution is prohibited.

Noriben (Twitter) : [https://twitter.com/noriben327](https://twitter.com/noriben327)

# Code License
[MIT](LICENSE.md)