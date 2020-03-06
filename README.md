# VRSkate
VRスケート（研究用プロジェクト）

VRスケート開発における研究用のリポジトリとなります。

スクリプト概要<br/>
・PathControl<br/>
本アプリにおけるメインスクリプトです。<br/>

BaseObject・・・HMD<br/>
TrackedObject・・・トラッカー<br/>
これらのTransformを取得し、その差分をOffsetとして記録し、
現在のフレームと前のフレームのOffsetを比較しその差分が一定値を超えることで加速する仕組みとなっています。<br/>

・SceneController<br/>
シーン遷移のスクリプトです。シーン遷移時の挙動を制御します。<br/>

・PlayerDataAsset<br/>
ラップタイムと滑走距離の記録用スクリプティングオブジェクトです。上記のスクリプトにアタッチして使用します。<br/>
