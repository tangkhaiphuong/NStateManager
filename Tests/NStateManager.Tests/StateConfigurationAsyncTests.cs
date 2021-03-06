﻿#region Copyright (c) 2018 Scott L. Carter
//
//Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in
//compliance with the License. You may obtain a copy of the License at
//http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software distributed under the License is 
//distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and limitations under the License.
#endregion
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NStateManager.Tests
{
    public class StateConfigurationAsyncTests
    {
        [Fact]
        public void AddAutoForwardTransition_throws_InvalidOperationException_if_AutoTransition_already_set()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            sut.AddAutoForwardTransition(SaleState.Complete, (sale, _) => Task.FromResult(result: true));

            Assert.Throws<InvalidOperationException>(() => sut.AddAutoForwardTransition(SaleState.Complete, (sale, _) => Task.FromResult(result: true)));
        }

        [Fact]
        public void AddAutoForwardTransition_throws_ArgumentNullException_if_Condition_null()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            Assert.Throws<ArgumentNullException>(() => sut.AddAutoForwardTransition(SaleState.Complete, condition: null));
        }

        [Fact]
        public void AddAutoForwardTransitionWRequest_throws_InvalidOperationException_if_AutoTransition_already_set()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            sut.AddAutoForwardTransition<string>(SaleState.Complete, (sale, stringParam, _) => Task.FromResult(result: true));

            Assert.Throws<InvalidOperationException>(() 
                => sut.AddAutoForwardTransition<string>(SaleState.Complete, (sale, stringParam, _) => Task.FromResult(result: true)));
        }

        [Fact]
        public void AddAutoForwardTransitionWRequest_throws_ArgumentNullException_if_Condition_null()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            Assert.Throws<ArgumentNullException>(() => sut.AddAutoForwardTransition<string>(SaleState.Complete, condition: null));
        }

        [Fact]
        public void AddAutoForwardTransitionWPreviousState_throws_ArgumentNullException_if_Condition_null()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            Assert.Throws<ArgumentNullException>(() => sut.AddAutoForwardTransition(SaleState.Complete, condition: null, previousState: SaleState.Open));
        }

        [Fact]
        public void AddAutoForwardTransitionWRequestPreviousState_throws_ArgumentNullException_if_Condition_null()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            Assert.Throws<ArgumentNullException>(() 
                => sut.AddAutoForwardTransition<string>(SaleState.Complete, condition: (null as Func<Sale, string, CancellationToken, Task<bool>>), previousState: SaleState.Open));
        }

        [Fact]
        public void AddAutoForwardTransitionWRequestPreviousState_adds_transition()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            sut.AddAutoForwardTransition<string>(SaleState.Complete, condition: (sale, s, _) => Task.FromResult(true), previousState: SaleState.Open);
        }

        [Fact]
        public void AddDynamicTransition_throws_ArgumentNullException_if_Function_null()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            Assert.Throws<ArgumentNullException>(() => sut.AddDynamicTransition(SaleEvent.AddItem, function: null));
        }

        [Fact]
        public void AddDynamicTransitionWRequest_throws_ArgumentNullException_if_Function_null()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            Assert.Throws<ArgumentNullException>(() => sut.AddDynamicTransition<string>(SaleEvent.AddItem, function: null));
        }

        [Fact]
        public void AddEntryAction_throws_ArgumentNullException_if_Action_null()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            Assert.Throws<ArgumentNullException>(() => sut.AddEntryAction(action: null));
        }

        [Fact]
        public void AddEntryAction_throws_InvalidOperationException_if_already_set()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            sut.AddEntryAction((sale, _) => Task.CompletedTask);

            Assert.Throws<InvalidOperationException>(() => sut.AddEntryAction((sale, _) => Task.CompletedTask));
        }

        [Fact]
        public void AddEntryActionWPrevious_throws_ArgumentNullException_if_Action_null()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            Assert.Throws<ArgumentNullException>(() => sut.AddEntryAction(action: null, previousState: SaleState.Open));
        }

        [Fact]
        public void AddEntryActionWPrevious_throws_InvalidOperationException_if_already_set()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            sut.AddEntryAction((sale, _) => Task.CompletedTask, previousState: SaleState.Open);

            Assert.Throws<InvalidOperationException>(() => sut.AddEntryAction((sale, _) => Task.CompletedTask, SaleState.Open));
        }

        [Fact]
        public void AddExitAction_throws_ArgumentNullException_if_Action_null()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            Assert.Throws<ArgumentNullException>(() => sut.AddExitAction(action: null));
        }

        [Fact]
        public void AddExitAction_throws_InvalidOperationException_if_already_set()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            sut.AddExitAction((sale, _) => Task.CompletedTask);

            Assert.Throws<InvalidOperationException>(() => sut.AddExitAction((sale, _) => Task.CompletedTask));
        }

        [Fact]
        public void AddAutoFallbackTransition_throws_InvalidOperationException_if_AutoTransition_already_set()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            sut.AddAutoFallbackTransition((sale, _) => Task.FromResult(result: true));

            Assert.Throws<InvalidOperationException>(() => sut.AddAutoFallbackTransition((sale, _) => Task.FromResult(result: true)));
        }

        [Fact]
        public void AddAutoFallbackTransition_throws_ArgumentNullException_if_Condition_null()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            Assert.Throws<ArgumentNullException>(() => sut.AddAutoFallbackTransition(condition: null));
        }

        [Fact]
        public void AddAutoFallbackTransitionWPreviousState_adds_transition()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            sut.AddAutoFallbackTransition(condition: (sale, _) => Task.FromResult(true), previousState: SaleState.Complete);
        }

        [Fact]
        public void AddAutoFallbackTransitionWRequest_throws_InvalidOperationException_if_AutoTransition_already_set()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            sut.AddAutoFallbackTransition<string>((sale, stringParam, _) => Task.FromResult(result: true));

            Assert.Throws<InvalidOperationException>(() => sut.AddAutoFallbackTransition<string>((sale, stringParam, _) 
                => Task.FromResult(result: true)));
        }

        [Fact]
        public void AddAutoFallbackTransitionWRequest_throws_ArgumentNullException_if_Condition_null()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            Assert.Throws<ArgumentNullException>(() => sut.AddAutoFallbackTransition<string>(condition: null));
        }

        [Fact]
        public void AddAutoFallbackTransitionWRequest_adds_transition()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            sut.AddAutoFallbackTransition<string>(condition: (sale, s, _) => Task.FromResult(true));
        }

        [Fact]
        public void AddAutoFallbackTransitionWPreviousState_throws_ArgumentNullException_if_Condition_null()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            Assert.Throws<ArgumentNullException>(() => sut.AddAutoFallbackTransition(condition: null, previousState: SaleState.Open));
        }

        [Fact]
        public void AddAutoFallbackTransitionWRequestPreviousState_throws_ArgumentNullException_if_Condition_null()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            Assert.Throws<ArgumentNullException>(() => sut.AddAutoFallbackTransition<string>(condition: null, previousState: SaleState.Open));
        }

        [Fact]
        public void AddAutoFallbackTransitionWRequestPreviousState_adds_transition()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            sut.AddAutoFallbackTransition<string>(condition: (sale, s, _) => Task.FromResult(true), previousState: SaleState.Open);
        }

        [Fact]
        public void AddReentryAction_throws_ArgumentNullException_if_Action_null()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            Assert.Throws<ArgumentNullException>(() => sut.AddReentryAction(action: null));
        }

        [Fact]
        public void AddReentryAction_throws_InvalidOperationException_if_already_set()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            sut.AddReentryAction((sale, _) => Task.CompletedTask);

            Assert.Throws<InvalidOperationException>(() => sut.AddReentryAction((sale, _) => Task.CompletedTask));
        }

        [Fact]
        public void AddSuperState_throws_ArgumentOutOfRangeException_if_configuredState_already_substate()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);
            var openStateConfig = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.Open
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            //First time works fine
            sut.AddSuperState(openStateConfig);

            //Second time blows up
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.AddSuperState(openStateConfig));
        }

        [Fact]
        public void AddSuperState_throws_ArgumentOutOfRangeException_if_substate_is_already_superstate()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);
            var openStateConfig = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.Open
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            //First time works fine
            openStateConfig.AddSuperState(sut);

            //Second time blows up
            Assert.Throws<ArgumentOutOfRangeException>(() => openStateConfig.AddSuperState(sut));
        }

        [Fact]
        public void AddTransitionWRequest_throws_ArgumentNullException_if_ConditionAsync_null()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            Assert.Throws<ArgumentNullException>(() => sut.AddTransition<string>(SaleEvent.AddItem, SaleState.Complete, conditionAsync: null));
        }

        [Fact]
        public void AddTriggerAction_throws_InvalidOperationException_if_already_set()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            sut.AddTriggerAction(SaleEvent.Pay, (sale, _) => Task.CompletedTask);

            Assert.Throws<InvalidOperationException>(() => sut.AddTriggerAction(SaleEvent.Pay, (sale, _) => Task.CompletedTask));
        }

        [Fact]
        public void AddTriggerAction_throws_ArgumentNullException_if_Action_null()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            Assert.Throws<ArgumentNullException>(() => sut.AddTriggerAction(SaleEvent.AddItem, action: null));
        }

        [Fact]
        public void AddTriggerActionWRequest_throws_InvalidOperationException_if_already_set()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            sut.AddTriggerAction<string>(SaleEvent.Pay, (sale, stringParam, _) => Task.CompletedTask);

            Assert.Throws<InvalidOperationException>(() => sut.AddTriggerAction<string>(SaleEvent.Pay, (sale, stringParam, _) => Task.CompletedTask));
        }

        [Fact]
        public void AddTriggerActionWRequest_throws_ArgumentNullException_if_Action_null()
        {
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale => sale.State
              , stateMutator: (sale, newState) => sale.State = newState);

            Assert.Throws<ArgumentNullException>(() => sut.AddTriggerAction<string>(SaleEvent.AddItem, action: null));
        }

        [Fact]
        public async Task ExecuteAutoTransitionAsync_executes_AutoTransition_based_on_previous_state_if_exists()
        {
            var sale = new Sale(saleID: 96) { State = SaleState.ChangeDue };
            var transitionResult = new StateTransitionResult<SaleState, SaleEvent>(SaleEvent.Pay
                , SaleState.Open
                , SaleState.Open
                , SaleState.ChangeDue
                , "lastTransitionName");
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale1 => sale.State
              , stateMutator: (sale1, newState) => sale.State = newState);
            sut.AddAutoForwardTransition(SaleState.Complete, (sale1, _) => Task.FromResult(result: true), previousState: SaleState.Open);
            var parameters = new ExecutionParameters<Sale, SaleEvent>(SaleEvent.ChangeGiven, sale);

            var autoTransitionResult = await sut.ExecuteAutoTransitionAsync(parameters, transitionResult);

            Assert.True(autoTransitionResult.WasSuccessful);
            Assert.Equal(SaleState.Complete, sale.State);
            Assert.Equal(SaleState.Complete, autoTransitionResult.CurrentState);
            Assert.Equal(SaleState.ChangeDue, autoTransitionResult.PreviousState);
            Assert.Equal(SaleState.Open, autoTransitionResult.StartingState);
        }

        [Fact]
        public async Task ExecuteAutoTransitionAsync_executes_AutoTransition_without_previous_state()
        {
            var sale = new Sale(saleID: 96) { State = SaleState.ChangeDue };
            var transitionResult = new StateTransitionResult<SaleState, SaleEvent>(SaleEvent.Pay
                , SaleState.Open
                , SaleState.Open
                , SaleState.ChangeDue
                , "lastTransitionName");
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale1 => sale.State
              , stateMutator: (sale1, newState) => sale.State = newState);
            sut.AddAutoForwardTransition(SaleState.Complete, (sale1, _) => Task.FromResult(result: true));
            var parameters = new ExecutionParameters<Sale, SaleEvent>(SaleEvent.ChangeGiven, sale);

            var autoTransitionResult = await sut.ExecuteAutoTransitionAsync(parameters, transitionResult);

            Assert.True(autoTransitionResult.WasSuccessful);
            Assert.Equal(SaleState.Complete, sale.State);
            Assert.Equal(SaleState.Complete, autoTransitionResult.CurrentState);
            Assert.Equal(SaleState.ChangeDue, autoTransitionResult.PreviousState);
            Assert.Equal(SaleState.Open, autoTransitionResult.StartingState);
        }

        [Fact]
        public async Task ExecuteAutoTransitionAsync_executes_AutoTransition_for_superState()
        {
            var sale = new Sale(saleID: 96) { State = SaleState.ChangeDue };
            var transitionResult = new StateTransitionResult<SaleState, SaleEvent>(SaleEvent.Pay
                , SaleState.Open
                , SaleState.Open
                , SaleState.ChangeDue
                , "lastTransitionName");
            var openState = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.Open
              , stateAccessor: sale1 => sale.State
              , stateMutator: (sale1, newState) => sale.State = newState);
            var changeDueState = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale1 => sale.State
              , stateMutator: (sale1, newState) => sale.State = newState);
            changeDueState.AddSuperState(openState);
            openState.AddAutoForwardTransition(SaleState.Complete, (sale1, _) => Task.FromResult(result: true));
            var parameters = new ExecutionParameters<Sale, SaleEvent>(SaleEvent.ChangeGiven, sale);

            var autoTransitionResult = await changeDueState.ExecuteAutoTransitionAsync(parameters, transitionResult);

            Assert.True(autoTransitionResult.WasSuccessful);
            Assert.Equal(SaleState.Complete, sale.State);
            Assert.Equal(SaleState.Complete, autoTransitionResult.CurrentState);
            Assert.Equal(SaleState.ChangeDue, autoTransitionResult.PreviousState);
            Assert.Equal(SaleState.Open, autoTransitionResult.StartingState);
        }

        [Fact]
        public void ExecuteAutoTransitionAsync_can_be_cancelled()
        {
            var sale = new Sale(saleID: 96) { State = SaleState.ChangeDue };
            var transitionResult = new StateTransitionResult<SaleState, SaleEvent>(SaleEvent.Pay
                , SaleState.Open
                , SaleState.Open
                , SaleState.ChangeDue
                , "lastTransitionName");
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale1 => sale.State
              , stateMutator: (sale1, newState) => sale.State = newState);
            sut.AddAutoForwardTransition(SaleState.Complete, (sale1, cancelToken) =>
            {
                do
                {
                    Task.Delay(millisecondsDelay: 123).Wait();
                } while (!cancelToken.IsCancellationRequested);

                return Task.FromResult(result: !cancelToken.IsCancellationRequested);
            });

            using (var cancelSource = new CancellationTokenSource())
            {
                var parameters = new ExecutionParameters<Sale, SaleEvent>(SaleEvent.ChangeGiven, sale, cancelSource.Token);
                StateTransitionResult<SaleState, SaleEvent> autoTransitionResult = null;

                var mutex = new Mutex(initiallyOwned: false);
                Task.Run(async () =>
                {
                    mutex.WaitOne();
                    autoTransitionResult = await sut.ExecuteAutoTransitionAsync(parameters, transitionResult);
                    mutex.ReleaseMutex();
                });

                try
                {
                    Task.Delay(millisecondsDelay: 2345).Wait();
                    cancelSource.Cancel();
                    mutex.WaitOne();
                }
                catch
                {
                    cancelSource.Cancel();
                }

                Assert.True(autoTransitionResult.WasCancelled);
                Assert.Equal(SaleState.ChangeDue, sale.State);
                Assert.Equal(SaleState.ChangeDue, autoTransitionResult.CurrentState);
                Assert.Equal(SaleState.Open, autoTransitionResult.PreviousState);
                Assert.Equal(SaleState.Open, autoTransitionResult.StartingState);
            }
        }

        [Fact]
        public async Task ExecuteEntryActionAsync_executes_EntryAction_based_on_previous_state()
        {
            var sale = new Sale(saleID: 96) { State = SaleState.ChangeDue };
            var transitionResult = new StateTransitionResult<SaleState, SaleEvent>(SaleEvent.Pay
                , SaleState.Open
                , SaleState.Open
                , SaleState.ChangeDue
                , "lastTransitionName");
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale1 => sale.State
              , stateMutator: (sale1, newState) => sale.State = newState);
            var entryActionFromOpenFired = false;
            sut.AddEntryAction((sale1, _) => { entryActionFromOpenFired = true; return Task.CompletedTask; }, SaleState.Open);
            var parameters = new ExecutionParameters<Sale, SaleEvent>(SaleEvent.Pay, sale);

            await sut.ExecuteEntryActionAsync(parameters, transitionResult);

            Assert.True(entryActionFromOpenFired);
        }

        [Fact]
        public async Task ExecuteEntryActionAsync_executes_EntryAction_without_previous_state()
        {
            var sale = new Sale(saleID: 96) { State = SaleState.ChangeDue };
            var transitionResult = new StateTransitionResult<SaleState, SaleEvent>(SaleEvent.Pay
                , SaleState.Open
                , SaleState.Open
                , SaleState.ChangeDue
                , "lastTransitionName");
            var sut = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale1 => sale.State
              , stateMutator: (sale1, newState) => sale.State = newState);
            var entryActionFromOpenFired = false;
            sut.AddEntryAction((sale1, _) => { entryActionFromOpenFired = true; return Task.CompletedTask; });
            var parameters = new ExecutionParameters<Sale, SaleEvent>(SaleEvent.Pay, sale);

            await sut.ExecuteEntryActionAsync(parameters, transitionResult);

            Assert.True(entryActionFromOpenFired);
        }

        [Fact]
        public async Task ExecuteReentryActionAsync_executes_superState_Action()
        {
            var sale = new Sale(saleID: 96) { State = SaleState.Open };
            var transitionResult = new StateTransitionResult<SaleState, SaleEvent>(SaleEvent.AddItem
                , SaleState.Open
                , SaleState.Open
                , SaleState.Open
                , "lastTransitionName");
            var openState = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.Open
              , stateAccessor: sale1 => sale.State
              , stateMutator: (sale1, newState) => sale.State = newState);
            var changeDueState = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale1 => sale.State
              , stateMutator: (sale1, newState) => sale.State = newState);
            changeDueState.AddSuperState(openState);
            var openEntryActionFromOpenFired = false;
            openState.AddReentryAction((sale1, _) => { openEntryActionFromOpenFired = true; return Task.CompletedTask; });
            var parameters = new ExecutionParameters<Sale, SaleEvent>(SaleEvent.Pay, sale);

            await changeDueState.ExecuteReentryActionAsync(parameters, transitionResult);

            Assert.True(openEntryActionFromOpenFired);
        }

        [Fact]
        public async Task ExecuteReentryActionAsync_executes_ReentryAction()
        {
            var sale = new Sale(saleID: 96) { State = SaleState.Open };
            var transitionResult = new StateTransitionResult<SaleState, SaleEvent>(SaleEvent.AddItem
                , SaleState.Open
                , SaleState.Open
                , SaleState.Open
                , "lastTransitionName");
            var openState = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.Open
              , stateAccessor: sale1 => sale.State
              , stateMutator: (sale1, newState) => sale.State = newState);
            var changeDueState = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale1 => sale.State
              , stateMutator: (sale1, newState) => sale.State = newState);
            changeDueState.AddSuperState(openState);
            var changeDueEntryActionFromOpenFired = false;
            changeDueState.AddReentryAction((sale1, _) => { changeDueEntryActionFromOpenFired = true; return Task.CompletedTask; });
            var parameters = new ExecutionParameters<Sale, SaleEvent>(SaleEvent.Pay, sale);

            await changeDueState.ExecuteReentryActionAsync(parameters, transitionResult);

            Assert.True(changeDueEntryActionFromOpenFired);
        }

        [Fact]
        public async Task ExecuteExitActionAsync_executes_based_on_next_state()
        {
            var sale = new Sale(saleID: 96) { State = SaleState.ChangeDue };
            var transitionResult = new StateTransitionResult<SaleState, SaleEvent>(SaleEvent.Pay
                , SaleState.Open
                , SaleState.Open
                , SaleState.ChangeDue
                , "lastTransitionName");
            var openState = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.Open
              , stateAccessor: sale1 => sale.State
              , stateMutator: (sale1, newState) => sale.State = newState);
            var openExitActionFired = false;
            openState.AddExitAction((sale1, _) => { openExitActionFired = true; return Task.CompletedTask; }, SaleState.ChangeDue);
            var parameters = new ExecutionParameters<Sale, SaleEvent>(SaleEvent.Pay, sale);

            await openState.ExecuteExitActionAsync(parameters, transitionResult);

            Assert.True(openExitActionFired);
        }

        [Fact]
        public async Task ExecuteExitActionAsync_executes_without_next_state()
        {
            var sale = new Sale(saleID: 96) { State = SaleState.ChangeDue };
            var transitionResult = new StateTransitionResult<SaleState, SaleEvent>(SaleEvent.Pay
                , SaleState.Open
                , SaleState.Open
                , SaleState.ChangeDue
                , "lastTransitionName");
            var openState = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.Open
              , stateAccessor: sale1 => sale.State
              , stateMutator: (sale1, newState) => sale.State = newState);

            var openExitActionFired = false;
            openState.AddExitAction((sale1, _) => { openExitActionFired = true; return Task.CompletedTask; });
            var parameters = new ExecutionParameters<Sale, SaleEvent>(SaleEvent.Pay, sale);

            await openState.ExecuteExitActionAsync(parameters, transitionResult);

            Assert.True(openExitActionFired);
        }

        [Fact]
        public async Task FireTriggerAsync_executes_superState_if_currentState_not_successful()
        {
            var sale = new Sale(saleID: 96) { State = SaleState.ChangeDue };
            var openState = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.Open
              , stateAccessor: sale1 => sale.State
              , stateMutator: (sale1, newState) => sale.State = newState);
            var openStatePayTriggerFired = false;
            openState
                .AddTriggerAction(SaleEvent.Pay, (sale1, _) => { openStatePayTriggerFired = true; return Task.CompletedTask; })
                .AddTransition(SaleEvent.Pay, SaleState.Complete, name: "openStatePay");
            var changeDueState = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale1 => sale.State
              , stateMutator: (sale1, newState) => sale.State = newState);
            changeDueState.AddSuperState(openState);
            var parameters = new ExecutionParameters<Sale, SaleEvent>(SaleEvent.Pay, sale);

            var result = await changeDueState.FireTriggerAsync(parameters);

            Assert.True(openStatePayTriggerFired);
            Assert.True(result.WasSuccessful);
            Assert.Equal(SaleState.Complete, sale.State);
            Assert.Equal(SaleState.Complete, result.CurrentState);
            Assert.Equal(SaleState.ChangeDue, result.PreviousState);
            Assert.Equal(SaleState.ChangeDue, result.StartingState);
            Assert.Equal("openStatePay", result.LastTransitionName);
        }

        [Fact]
        public void IsInState_returns_True_if_in_given_state()
        {
            var openState = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.Open
              , stateAccessor: sale1 => sale1.State
              , stateMutator: (sale1, newState) => sale1.State = newState);

            Assert.True(openState.IsInState(SaleState.Open));
            Assert.False(openState.IsInState(SaleState.Complete));
        }

        [Fact]
        public void IsInState_returns_True_if_in_given_state_is_subState()
        {
            var openState = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.Open
              , stateAccessor: sale1 => sale1.State
              , stateMutator: (sale1, newState) => sale1.State = newState);
            var changeDueState = new StateConfigurationAsync<Sale, SaleState, SaleEvent>(SaleState.ChangeDue
              , stateAccessor: sale1 => sale1.State
              , stateMutator: (sale1, newState) => sale1.State = newState);
            changeDueState.AddSuperState(openState);

            Assert.True(changeDueState.IsInState(SaleState.Open));
            Assert.True(changeDueState.IsInState(SaleState.ChangeDue));
            Assert.True(openState.IsInState(SaleState.Open));
            Assert.False(openState.IsInState(SaleState.ChangeDue));
        }
    }

    public class TestStateConfigurationAsync<T, TState, TTrigger> : StateConfigurationAsync<T, TState, TTrigger>
        where TState : IComparable
    {
        public TestStateConfigurationAsync(TState state, Func<T, TState> stateAccessor, Action<T, TState> stateMutator)
            : base(state, stateAccessor, stateMutator)
        { }
    }
}
