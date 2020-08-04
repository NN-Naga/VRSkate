# VRSkate
VRスケート（研究用プロジェクト）

VRスケート開発用のリポジトリとなります。


スクリプト概要<br/>

（ゲームのシーンで使用）
・GameSceneController
ゲームのシーン遷移を行うスクリプトです。

・PlayerData.cs
・PlayerDataAsset.asset
ハイスコアを記録するためのスクリプトです。


・PathControl<br/>
前進動作用スクリプトです。<br/>
「ITween」プラグイン（https://assetstore.unity.com/packages/tools/animation/itween-84?locale=ja-JP）を使用しており、プラグイン内のスクリプトの「TweenPath」と併用して使用します。
ITweenPathでパス（経路）を設定し、本スクリプトで経路移動の制御を行います。
本アプリでは、白ポールフラグの位置がパスの開始および終了地点となっており、そこを通過することによりラップ数がカウントされ、ステージが更新されます。

BaseObject・・・HMD<br/>
TrackedObject・・・トラッカー<br/>
これらのTransformを取得し、その差分をOffsetとして記録し、
現在のフレームと前のフレームのOffsetを比較しその差分が一定値を超えることで加速する仕組みとなっています。<br/>


・PlayerControl
横動作用スクリプトです。障害物にぶつかった際や、爆発に巻き込まれた際の挙動も設定されております。

・SceneController<br/>
シーン遷移のスクリプトです。シーン遷移時の挙動を制御します。<br/>

・PlayerDataAsset<br/>
ラップタイムと滑走距離の記録用スクリプティングオブジェクトです。上記のスクリプトにアタッチして使用します。<br/>

・StageGenerator
ステージ生成用スクリプトです。StageTipsで作成されたステージを設定し、StartTipIndexで最初に生成するステージを設定し、PreInstantiateで前方に生成するステージ数を設定します。
本アプリでは全4ステージの枠があり、前方より3ステージ目を繰り返し周回する仕組みとなっております。スタート時点では前方より3ステージ分が生成されます。ラップ数を重ねる度にステージが更新され、前方より3ステージ分が1つ後ろにずれ、最前方にステージが1つ生成され、最後方のステージは削除されます。



（ステージ作成時に使用）
・EnemyController
敵の行動操作を設定するスクリプトです。敵オブジェクト（スノーマンなど）にアタッチして使用します。

・ExplosionController
ボムの挙動を設定するスクリプトです。爆破範囲をSphireColliderで設定することができます。ボムオブジェクトにアタッチして使用します。

・ExplosionIndicator
領域内の侵入でボムを赤く点滅させるスクリプトです。領域範囲をBoxColliderで設定することができます。ボムの子オブジェクト「Indicator」にアタッチして使用します。

・ExplosionTrigger
領域内の侵入でボムを爆発させるスクリプトです。領域範囲をBoxColliderで設定することができます。ボムの子オブジェクト「BombReach」にアタッチして使用します。

・ItemPoint
障害物の位置を設定するスクリプトです。障害物オブジェクトに設定します。




＊＊＊Editorフォルダ＊＊＊
・EnemyControlEditor
障害物の回転設定、周回移動設定、およびその両方の設定を切り替えてインスペクタ上で設定できるエディタ拡張スクリプトです。



＊＊＊PotableOnlyフォルダ＊＊＊
上記スクリプトをポータブル版で使用するために改変したスクリプトです。

・GameSceneControllerPotable
ゲームのシーン遷移を行うポータブル版用スクリプトです。内容はGameSceneControllerと同様です。

・PathControlPotable
前進動作用スクリプトです。内容はPathControlと同様です。

・PlayerControlPotable
横動作用スクリプトです。内容はPlayerControlと同様です。

・StageGeneratorPotable
ステージ生成用スクリプトです。内容はStageGeneratorと同様です。



＊＊＊今後の展望＊＊＊
・ウインドウサイズ固定設定の実装
・難易度選択の実装
・セーブデータ、およびハイスコアデータの保存、読み出しの実装
・有利アイテムの実装、ポイントスコアの実装
・ポータブル版におけるプレイヤーモデルの実装
・ステージの改良
など
