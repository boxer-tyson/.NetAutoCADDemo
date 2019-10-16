using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.Runtime;

namespace chap22
{
    public class EllipseJig : EntityJig
    {
        // 声明全局变量.
        private Point3d mCenterPt, mMajorPt;
        private Vector3d mNormal, mMajorAxis;
        private int mPromptCounter;
        private double mRadiusRatio, radiusRatio;
        private double startAng, endAng, ang1, ang2;

        // 派生类的构造函数.
        public EllipseJig(Point3d center, Vector3d vec)
            : base(new Ellipse())
        {
            mCenterPt = center;
            mNormal = vec;
        }

        // Sampler函数用于检测用户的输入.
        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            if (mPromptCounter == 0)
            {
                // 定义一个点拖动交互类.
                JigPromptPointOptions optJigPoint = new JigPromptPointOptions("\n请指定椭圆弧轴上一点");
                // 设置拖拽的光标类型.
                optJigPoint.Cursor = CursorType.RubberBand;
                // 设置拖动光标基点.
                optJigPoint.BasePoint = mCenterPt;
                optJigPoint.UseBasePoint = true;
                // 用AcquirePoint函数得到用户输入的点.
                PromptPointResult resJigPoint = prompts.AcquirePoint(optJigPoint);
                Point3d curPt = resJigPoint.Value;
                if (curPt != mMajorPt)
                    // 
                    mMajorPt = curPt;
                else
                    return SamplerStatus.NoChange;
                if (resJigPoint.Status == PromptStatus.Cancel)
                    return SamplerStatus.Cancel;
                else
                    return SamplerStatus.OK;
            }
            else if (mPromptCounter == 1)
            {
                // 定义一个距离拖动交互类.
                JigPromptDistanceOptions optJigDis = new JigPromptDistanceOptions("\n请指定另一条半轴的长度");
                // 设置对拖拽的约束:禁止输入零和负值.
                optJigDis.UserInputControls = UserInputControls.NoZeroResponseAccepted | UserInputControls.NoNegativeResponseAccepted;
                // 设置拖拽的光标类型.
                optJigDis.Cursor = CursorType.RubberBand;
                // 设置拖动光标基点.
                optJigDis.BasePoint = mCenterPt;
                optJigDis.UseBasePoint = true;
                // 用AcquireDistance函数得到用户输入的距离值.
                PromptDoubleResult resJigDis = prompts.AcquireDistance(optJigDis);
                double mRadiusRatioTemp = resJigDis.Value;
                if (mRadiusRatioTemp != mRadiusRatio)
                    // 保存当前距离值.
                    mRadiusRatio = mRadiusRatioTemp;
                else
                    return SamplerStatus.NoChange;
                if (resJigDis.Status == PromptStatus.Cancel)
                    return SamplerStatus.Cancel;
                else
                    return SamplerStatus.OK;
            }
            else if (mPromptCounter == 2)
            {
                // 设置椭圆弧0度基准角.
                double baseAng;
                Vector2d mMajorAxis2d = new Vector2d(mMajorAxis.X, mMajorAxis.Y);
                if (radiusRatio < 1)
                    baseAng = mMajorAxis2d.Angle;
                else
                    baseAng = mMajorAxis2d.Angle + 0.5 * Math.PI;
                // 设置系统变量“ANGBASE”.
                Application.SetSystemVariable("ANGBASE", baseAng);
                // 定义一个角度拖动交互类.
                JigPromptAngleOptions optJigAngle1 = new JigPromptAngleOptions("\n请指定椭圆弧的起始角度");
                // 设置拖拽的光标类型.
                optJigAngle1.Cursor = CursorType.RubberBand;
                // 设置拖动光标基点.
                optJigAngle1.BasePoint = mCenterPt;
                optJigAngle1.UseBasePoint = true;
                // 用AcquireAngle函数得到用户输入的角度值.
                PromptDoubleResult resJigAngle1 = prompts.AcquireAngle(optJigAngle1);
                ang1 = resJigAngle1.Value;
                if (startAng != ang1)
                    // 保存当前角度值.
                    startAng = ang1;
                else
                    return SamplerStatus.NoChange;
                if (resJigAngle1.Status == PromptStatus.Cancel)
                    return SamplerStatus.Cancel;
                else
                    return SamplerStatus.OK;
            }
            else if (mPromptCounter == 3)
            {
                // 定义一个角度拖动交互类.
                JigPromptAngleOptions optJigAngle2 = new JigPromptAngleOptions("\n请指定椭圆弧的终止角度");
                // 设置拖拽的光标类型.
                optJigAngle2.Cursor = CursorType.RubberBand;
                // 设置拖动光标基点.
                optJigAngle2.BasePoint = mCenterPt;
                optJigAngle2.UseBasePoint = true;
                // 用AcquireAngle函数得到用户输入的角度值.
                PromptDoubleResult resJigAngle2 = prompts.AcquireAngle(optJigAngle2);
                ang2 = resJigAngle2.Value;
                if (endAng != ang2)
                    // 保存当前角度值.
                    endAng = ang2;
                else
                    return SamplerStatus.NoChange;
                if (resJigAngle2.Status == PromptStatus.Cancel)
                    return SamplerStatus.Cancel;
                else
                    return SamplerStatus.OK;
            }
            else
                return SamplerStatus.NoChange;
        }

        // Update函数用于刷新屏幕上显示的图形.
        protected override bool Update()
        {
            if (mPromptCounter == 0)
            {
                // 第一次拖拽时，椭圆的半径比为1，屏幕上显示的是一个圆.
                radiusRatio = 1;
                mMajorAxis = mMajorPt - mCenterPt;
                startAng = 0;
                endAng = 2 * Math.PI;
            }
            else if (mPromptCounter == 1)
                // 第二次拖拽时，修改了椭圆的半径比，屏幕上显示的是一个完整椭圆.
                radiusRatio = mRadiusRatio / mMajorAxis.Length;
            else if (mPromptCounter == 2)
                // 第三次拖拽时，修改了椭圆的起初角度，屏幕上显示的是一个终止角度为360度的椭圆弧.
                startAng = ang1;
            else if (mPromptCounter == 3)
                // 第四次拖拽时，修改了椭圆的终止角度，屏幕上显示的是一个最终的椭圆弧.
                endAng = ang2;
            try
            {
                if (radiusRatio < 1)
                    // 更新椭圆的参数.
                    ((Ellipse)(Entity)).Set(mCenterPt, mNormal, mMajorAxis, radiusRatio, startAng, endAng);
                else
                {
                    // 如另一条半轴长度超过椭圆弧长轴方向矢量的长度，则要重新定义椭圆弧长轴方向矢量的方向和长度.
                    Vector3d mMajorAxis2 = mMajorAxis.RotateBy(0.5 * Math.PI, Vector3d.ZAxis).DivideBy(1 / radiusRatio);
                    // 更新椭圆的参数.
                    ((Ellipse)(Entity)).Set(mCenterPt, mNormal, mMajorAxis2, 1 / radiusRatio, startAng, endAng);
                }
            }
            catch
            {
                // '此处不需要处理.
            }
            return true;
        }

        // GetEntity函数用于得到派生类的实体.
        public Entity GetEntity()
        {
            return Entity;
        }

        // setPromptCounter过程用于控制不同的拖拽.
        public void setPromptCounter(int i)
        {
            mPromptCounter = i;
        }
    }

    public class EntityJig_Ellipse
    {
        [CommandMethod("JigEllipse")]
        public void CreateJigEllipse()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            // 备份系统变量“ANGBASE”.
            object oldAngBase = Application.GetSystemVariable("ANGBASE");
            // 普通的点交互操作.
            PromptPointOptions optPoint = new PromptPointOptions("\n请指定椭圆弧的圆心:");
            PromptPointResult resPoint = ed.GetPoint(optPoint);
            if (resPoint.Status != PromptStatus.OK)
                return;
            // 定义一个EntityJig派生类的实例.
            EllipseJig myJig = new EllipseJig(resPoint.Value, Vector3d.ZAxis);
            // 第一次拖拽.
            myJig.setPromptCounter(0);
            PromptResult resJig = ed.Drag(myJig);
            if (resJig.Status == PromptStatus.OK)
            {
                // 第二次拖拽.
                myJig.setPromptCounter(1);
                resJig = ed.Drag(myJig);
                if (resJig.Status == PromptStatus.OK)
                {
                    // 第三次拖拽.
                    myJig.setPromptCounter(2);
                    resJig = ed.Drag(myJig);
                    if (resJig.Status == PromptStatus.OK)
                    {
                        // 第四次拖拽.
                        myJig.setPromptCounter(3);
                        resJig = ed.Drag(myJig);
                        if (resJig.Status == PromptStatus.OK)
                        {
                            using (Transaction trans = db.TransactionManager.StartTransaction())
                            {
                                BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                                btr.AppendEntity(myJig.GetEntity());
                                trans.AddNewlyCreatedDBObject(myJig.GetEntity(), true);
                                trans.Commit();
                            }
                        }
                    }
                }
            }
            // 还原系统变量“ANGBASE”.
            Application.SetSystemVariable("ANGBASE", oldAngBase);
        }
    }
}
