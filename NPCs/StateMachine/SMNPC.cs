﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace TemplateMod2.NPCs.StateMachine {
    /// <summary>
    /// 基于状态机的ModProjectile类，一定要先在Initialize里注册弹幕的状态才能使用哦
    /// </summary>
    public abstract class SMNPC : ModNPC {
        public NPCState currentState => npcStates[State - 1];
        private List<NPCState> npcStates = new List<NPCState>();
        private Dictionary<string, int> stateDict = new Dictionary<string, int>();
        private int State {
            get { return (int)npc.ai[0]; }
            set { npc.ai[0] = (int)value; }
        }
        public int Timer {
            get { return (int)npc.ai[1]; }
            set { npc.ai[1] = value; }
        }
        public Player TargetPlayer {
            get { return Main.player[npc.target]; }
        }
        /// <summary>
        /// 把当前状态变为指定的弹幕状态实例
        /// </summary>
        /// <typeparam name="T">注册过的<see cref="NPCState"/>类名</typeparam>
        public void SetState<T>() where T : NPCState {
            var name = typeof(T).FullName;
            if (!stateDict.ContainsKey(name)) throw new ArgumentException("这个状态并不存在");
            State = stateDict[name];
        }
        /// <summary>
        /// 判断NPC是否处于某个状态
        /// </summary>
        /// <typeparam name="T">注册过的<see cref="NPCState"/>类名</typeparam>
        /// <returns></returns>
        public bool AtState<T>() where T : NPCState {
            var name = typeof(T).FullName;
            if (!stateDict.ContainsKey(name)) throw new ArgumentException("这个状态并不存在");
            return State == stateDict[name];
        }
        /// <summary>
        /// 注册状态
        /// </summary>
        /// <typeparam name="T">需要注册的<see cref="NPCState"/>类</typeparam>
        /// <param name="state">需要注册的<see cref="NPCState"/>类的实例</param>
        protected void RegisterState<T>() where T : NPCState {
            var name = typeof(T).FullName;
            if (stateDict.ContainsKey(name)) throw new ArgumentException("这个状态已经注册过了");
            var state = (T)Activator.CreateInstance(typeof(T), new[] { this });
            npcStates.Add(state);
            stateDict.Add(name, npcStates.Count);
        }

        /// <summary>
        /// 初始化函数，用于注册弹幕状态
        /// </summary>
        public abstract void Initialize();
        /// <summary>
        /// 我把AI函数封住了，这样在子类无法重写AI函数，只能用before和after函数
        /// </summary>
        public sealed override void AI() {
            if (State == 0) {
                Initialize();
                State = 1;
            }
            AIBefore();
            currentState.AI(this);
            AIAfter();
        }
        /// <summary>
        /// 在状态机执行之前要执行的代码
        /// </summary>
        public virtual void AIAfter() { }
        /// <summary>
        /// 在状态机执行之后要执行的代码
        /// </summary>
        public virtual void AIBefore() { }
    }
}
