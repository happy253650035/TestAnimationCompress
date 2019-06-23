## 动画资源压缩测试工程

#### AnimationCompress 
`通过ToolAnimationCompress工具直接处理后的资源，将会裁切float至3位，删除genericBindings、m_EditorCurves、m_EulerEditorCurves标签内的对应内容`
#### AnimationUnCompress 
`为经过压缩处理的资源`
#### AnimationFakeCompress 
`只移除genericBindings、m_EditorCurves、m_EulerEditorCurves标签内对应内容不做float的裁切`
#### AnimationTrueCompress 
`只做float的裁切，不移除genericBindings、m_EditorCurves、m_EulerEditorCurves标签内的对应内容`

## 结论
只有通过float的裁切对Animation的压缩才是有效的，genericBindings、m_EditorCurves、m_EulerEditorCurves置空不会影响Animation产出的AB包的大小，说明Unity打AB包时会自动裁切genericBindings、m_EditorCurves、m_EulerEditorCurves标签内的对应内容
